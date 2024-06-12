using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_test_task_backend.Interfaces;
using net_test_task_backend.Models;

namespace net_test_task_backend.Controllers;

[Route("api/about")]
[ApiController]
public class AboutController : ControllerBase
{
    private readonly IAboutRepository _aboutRepository;

    public AboutController(IAboutRepository aboutRepository)
    {
        _aboutRepository = aboutRepository;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUrlById([FromRoute] Guid id)
    {
        var expectedAbout = await _aboutRepository.GetAboutById(id);

        if (expectedAbout == null) return NotFound(new { message = "Such Information could not be found" });
        return Ok(expectedAbout);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddAbout([FromBody] About about)
    {
        var addedAbout = await _aboutRepository.AddAbout(about);

        return CreatedAtAction(nameof(AddAbout), addedAbout);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateAbout([FromRoute] Guid id, [FromBody] About about)
    {
        var updatedAbout = await _aboutRepository.UpdateAbout(id, about);

        if (updatedAbout == null) return NotFound(new { message = "No such information has been found" });

        return Ok(updatedAbout);
    }
}
