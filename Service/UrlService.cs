using net_test_task_backend.Dtos.Url;
using net_test_task_backend.Interfaces;
using net_test_task_backend.Models;

namespace net_test_task_backend.Service;

public class UrlService: IUrlService
{
    private string GenerateShortenUrl()
    {
        var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var numberOfChars = new char[6];
        var random = new Random();

        for (int i = 0; i < numberOfChars.Length; i++)
        {
            numberOfChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(numberOfChars);
    }

    public Url CreateUrlObject(UserUrlDto userUrl)
    {
        var shortenedUrl = GenerateShortenUrl();

        var url = new Url
        {
            OriginalUrl = userUrl.OriginalUrl,
            ShortenedVersion = shortenedUrl,
            CreatedBy = userUrl.CreatedBy,
            ExpirationDate = DateTime.UtcNow,
        };

        return url;
    }
}
