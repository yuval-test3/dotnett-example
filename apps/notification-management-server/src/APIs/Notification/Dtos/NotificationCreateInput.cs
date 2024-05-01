namespace NotificationManagement.APIs.Dtos;

public class NotificationCreateInput
{
    public DateTime CreatedAt { get; set; }
    public string? Message { get; set; }
    public string? Recipient { get; set; }
    public string? Title { get; set; }
}
