using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_test_task_backend.Dtos.Url;
using net_test_task_backend.Interfaces;

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

    [HttpGet]
    public async Task<IActionResult> GetAllUrls()
    {
        var allUrls = await _urlRepository.GetAllUrls();

        return Ok(allUrls);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetUrlById([FromRoute] Guid id)
    {
        var expectedUrl = await _urlRepository.GetUrlById(id);

        if (expectedUrl == null) return NotFound(new { message = "Such Url could not be found" });

        return Ok(expectedUrl);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddUrl([FromBody] UserUrlDto url)
    {
        var checkIfUrlIsNotNull = _urlService.CheckIfUrlIsNullOrEmpty(url.OriginalUrl);

        if (checkIfUrlIsNotNull) return BadRequest(new { message = "Url can not be empty" });

        var checkIfUrlDoesNotExist = await _urlRepository.CheckIfUrlDoesNotExist(url.OriginalUrl);

        if (!checkIfUrlDoesNotExist) return BadRequest(new { message = "Such Url already exists" });

        var createUrl = _urlService.CreateUrlObject(url);

        var addedUrl = await _urlRepository.AddUrl(createUrl);

        return CreatedAtAction(nameof(AddUrl), addedUrl);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteUrl([FromRoute] Guid id)
    {
        var deletedUrl = await _urlRepository.DeleteUrl(id);

        if (deletedUrl == null) return NotFound(new { message = "Such Url could not be found" });

        return NoContent();
    }

    [HttpGet("{shortenUrl}")]
    public async Task<IActionResult> ShortToFullUrlRedirect([FromRoute] string shortenUrl)
    {
        var originalUrl = await _urlRepository.GetOriginalUrl(shortenUrl);

        if (originalUrl == null)
        {
            var frontendUrl = _configuration.GetValue<string>("Frontend:Url");
            return Redirect(frontendUrl);
        }

        return Redirect(originalUrl);
    }
}
