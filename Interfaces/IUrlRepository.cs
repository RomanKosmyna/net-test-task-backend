using net_test_task_backend.Models;

namespace net_test_task_backend.Interfaces;

public interface IUrlRepository
{
    Task<List<Url>> GetAllUrls();
    Task<Url?> GetUrlById(Guid id);
    Task<Url> AddUrl(Url url);
    Task<Url?> DeleteUrl(Guid id);
    Task<string?> ShortToFullUrlRedirect(string shortenUrl);
}
