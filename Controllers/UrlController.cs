using Microsoft.AspNetCore.Mvc;
using net_test_task_backend.Interfaces;
using net_test_task_backend.Models;

namespace net_test_task_backend.Controllers;

[Route("api/url")]
[ApiController]
public class UrlController : ControllerBase
{
    private readonly IUrlRepository _urlRepository;
    private readonly IUrlService _urlService;

    public UrlController(IUrlRepository urlRepository, IUrlService urlService)
    {
        _urlRepository = urlRepository;
        _urlService = urlService;
    }

    [HttpGet("urls")]
    public async Task<IActionResult> GetAllUrls()
    {
        var allUrls = await _urlRepository.GetAllUrls();

        return Ok(allUrls);
    }

    [HttpGet("url/{id:guid}")]
    public async Task<IActionResult> GetUrlById([FromRoute] Guid id)
    {
        var expectedUrl = await _urlRepository.GetUrlById(id);

        if (expectedUrl == null) return NotFound(new { message = "Such Url could not be found" });

        return Ok(expectedUrl);
    }

    [HttpPost("url")]
    public async Task<IActionResult> AddUrl([FromBody] Url url)
    {
        var addedUrl = await _urlRepository.AddUrl(url);

        return CreatedAtAction(nameof(AddUrl), addedUrl);
    }

    [HttpDelete("url/{id:guid}")]
    public async Task<IActionResult> DeleteUrl([FromRoute] Guid id)
    {
        var deletedUrl = await _urlRepository.DeleteUrl(id);

        if (deletedUrl == null) return NotFound(new { message = "Such Url could not be found" });

        return NoContent();
    }

    [HttpGet("shortenUrl/{shortenUrl}")]
    public async Task<IActionResult> ShortToFullUrlRedirect(string shortenUrl)
    {
        var originalUrl = await _urlRepository.ShortToFullUrlRedirect(shortenUrl);

        if (originalUrl == null) return NotFound(new { message = "Such Url could not be found" });

        return Redirect(originalUrl);
    }
}
