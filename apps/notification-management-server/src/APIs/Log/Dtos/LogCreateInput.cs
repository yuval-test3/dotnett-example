namespace NotificationManagement.APIs.Dtos;

public class LogCreateInput
{
    public DateTime CreatedAt { get; set; }
    public string? Level { get; set; }
    public string? Timestamp { get; set; }
    public string? Message { get; set; }
}
