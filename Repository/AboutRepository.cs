using net_test_task_backend.Data;
using net_test_task_backend.Interfaces;
using net_test_task_backend.Models;

namespace net_test_task_backend.Repository;

public class AboutRepository : IAboutRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AboutRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<About?> GetAboutById(Guid id)
    {
        return await _dbContext.Abouts.FindAsync(id) ?? null;
    }

    public async Task<About> AddAbout(About about)
    {
        await _dbContext.Abouts.AddAsync(about);
        await _dbContext.SaveChangesAsync();

        return about;
    }
    public async Task<About?> UpdateAbout(Guid id, About updatedAbout)
    {
        var expectedAbout = await _dbContext.Abouts.FindAsync(id);

        if (expectedAbout == null) return null;

        expectedAbout.Description = updatedAbout.Description;

        await _dbContext.SaveChangesAsync();

        return updatedAbout;
    }
}