using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using net_test_task_backend.Dtos.Account;
using net_test_task_backend.Models;

namespace net_test_task_backend.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
                return Ok(new CreatedUserDto
                {
                    UserName = user.UserName,
                    Token = "testing"
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
}
