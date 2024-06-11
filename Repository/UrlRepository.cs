using Microsoft.EntityFrameworkCore;
using net_test_task_backend.Data;
using net_test_task_backend.Interfaces;
using net_test_task_backend.Models;

namespace net_test_task_backend.Repository;

public class UrlRepository : IUrlRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UrlRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Url>> GetAllUrls()
    {
        var allUrls = await _dbContext.Urls.ToListAsync();

        return allUrls;
    }

    public async Task<Url?> GetUrlById(Guid id)
    {
        return await _dbContext.Urls.FindAsync(id) ?? null;
    }

    public async Task<Url> AddUrl(Url url)
    {
        await _dbContext.Urls.AddAsync(url);
        await _dbContext.SaveChangesAsync();

        return url;
    }

    public async Task<Url?> DeleteUrl(Guid id)
    {
        var expectedUrl = await _dbContext.Urls.FindAsync(id);

        if (expectedUrl == null) return null;

        _dbContext.Urls.Remove(expectedUrl);
        await _dbContext.SaveChangesAsync();

        return expectedUrl;
    }

    public async Task<string?> ShortToFullUrlRedirect(string shortenUrl)
    {
        var findAppropriateUrl = await _dbContext.Urls
        .FirstOrDefaultAsync(u => u.ShortenedVersion == shortenUrl);

        if (findAppropriateUrl == null) return null;

        return findAppropriateUrl.OriginalUrl;
    }
}
