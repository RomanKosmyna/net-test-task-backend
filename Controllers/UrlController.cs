using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_test_task_backend.Dtos.Url;
using net_test_task_backend.Interfaces;
using net_test_task_backend.Models;

namespace net_test_task_backend.Controllers;

[Route("api/url")]
[ApiController]
public class UrlController : ControllerBase
{
    private readonly IUrlRepository _urlRepository;
    private readonly IUrlService _urlService;
    private readonly IConfiguration _configuration;

    public UrlController(IUrlRepository urlRepository, IUrlService urlService, IConfiguration configuration)
    {
        _urlRepository = urlRepository;
        _urlService = urlService;
        _configuration = configuration;
    }

    [HttpGet("urls")]
    public async Task<IActionResult> GetAllUrls()
    {
        var allUrls = await _urlRepository.GetAllUrls();

        return Ok(allUrls);
    }

    [HttpGet("url/{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetUrlById([FromRoute] Guid id)
    {
        var expectedUrl = await _urlRepository.GetUrlById(id);

        if (expectedUrl == null) return NotFound(new { message = "Such Url could not be found" });

        return Ok(expectedUrl);
    }

    [HttpPost("url")]
    [Authorize]
    public async Task<IActionResult> AddUrl([FromBody] UserUrlDto url)
    {
        var createUrl = _urlService.CreateUrlObject(url);

        var addedUrl = await _urlRepository.AddUrl(createUrl);

        return CreatedAtAction(nameof(AddUrl), addedUrl);
    }

    [HttpDelete("url/{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteUrl([FromRoute] Guid id)
    {
        var deletedUrl = await _urlRepository.DeleteUrl(id);

        if (deletedUrl == null) return NotFound(new { message = "Such Url could not be found" });

        return NoContent();
    }

    [HttpGet("{shortenUrl}")]
    public async Task<IActionResult> ShortToFullUrlRedirect(string shortenUrl)
    {
        var originalUrl = await _urlRepository.ShortToFullUrlRedirect(shortenUrl);

        if (originalUrl == null)
        {
            var frontendUrl = _configuration.GetValue<string>("Frontend:Url");
            return Redirect(frontendUrl);
        }

        return Redirect(originalUrl);
    }
}
