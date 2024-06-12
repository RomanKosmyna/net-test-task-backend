namespace net_test_task_backend.Models;

public class Url
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string OriginalUrl { get; set; }
    public string ShortenedVersion { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ExpirationDate { get; set; }
}
