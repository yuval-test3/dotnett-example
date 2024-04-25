namespace CustomerServiceManagement.APIs.Dtos;

public class FeedbackDto
{
    public DateTime CreatedAt { get; set; }
    public string? Content { get; set; }
    public string? Rating { get; set; }
    public CustomerDto CustomerId { get; set; }
}
