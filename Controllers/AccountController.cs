﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_test_task_backend.Dtos.Account;
using net_test_task_backend.Models;
using net_test_task_backend.Service;

namespace net_test_task_backend.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDto userRegisterDto)
    {
        var user = new AppUser
        {
            UserName = userRegisterDto.UserName,
        };

        var createUser = await _userManager.CreateAsync(user, userRegisterDto.Password);

        if (createUser.Succeeded)
        {
            var assignUserRole = await _userManager.AddToRoleAsync(user, "User");
            
            if (assignUserRole.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();

                return Ok(new CreatedUserDto
                {
                    UserName = user.UserName,
                    Token = _tokenService.CreateToken(user, role)
                });
            }
            else
            {
                return StatusCode(500, assignUserRole.Errors);
            }
        }
        else
        {
            return StatusCode(500, createUser.Errors);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLoginDto userLoginDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userLoginDto.UserName);

        if (user == null) return Unauthorized("Invalid username");

        var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);

        if (!result.Succeeded) return Unauthorized("Username or password are incorrect.");

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault();

        return Ok(new CreatedUserDto
        {
            UserName = user.UserName,
            Token = _tokenService.CreateToken(user, role)
        });
    }

    [HttpPost("finduseridbytoken")]
    [Authorize]
    public async Task<IActionResult> FindUserIdByToken([FromBody] string token)
    {
        var username = _tokenService.GetUsernameFromToken(token);

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);

        if (user == null) return NotFound(new { message = "Such user could not be found" });

        return Ok(user.Id);
    }
}
