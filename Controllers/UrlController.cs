using Microsoft.AspNetCore.Mvc;
using net_test_task_backend.Interfaces;

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

    [HttpPost("url")]
    public async Task<IActionResult> ShortenUrl([FromBody] string originalUrl)
    {
        Serilog.Log.Logger.Information("test");
        var url = _urlService.CreateUrlObject(originalUrl);

        await _urlRepository.AddShortenUrl(url);

        return Ok(new { shortenedUrl = $"https://shorturl/{url.ShortenedVersion}" });
    }

    [HttpGet("urls")]
    public async Task<IActionResult> GetAllUrls()
    {
        var allUrls = await _urlRepository.GetAllUrls();

        return Ok(allUrls);
    }
}
