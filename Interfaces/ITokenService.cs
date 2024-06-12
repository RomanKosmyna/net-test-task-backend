using net_test_task_backend.Models;

namespace net_test_task_backend.Service;

public interface ITokenService
{
    string CreateToken(AppUser user, string token);
    string GetUsernameFromToken(string token);
}
