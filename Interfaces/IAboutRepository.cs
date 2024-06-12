using net_test_task_backend.Models;

namespace net_test_task_backend.Interfaces;

public interface IAboutRepository
{
    Task<About?> GetAboutById(Guid id);
    Task<About> AddAbout(About about);
}
