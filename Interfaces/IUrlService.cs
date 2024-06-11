using net_test_task_backend.Dtos.Url;
using net_test_task_backend.Models;

namespace net_test_task_backend.Interfaces;

public interface IUrlService
{
    Url CreateUrlObject(UserUrlDto userUrl);
}
