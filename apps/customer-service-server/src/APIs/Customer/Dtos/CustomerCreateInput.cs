namespace CustomerServiceManagement.APIs.Dtos;

public class CustomerCreateInput
{
    public DateTime CreatedAt { get; set; }
    public string? Phone { get; set; }
    public string? Name { get; set; }
    public ICollection<FeedbackDto>? Feedbacks { get; set; }
    public ServiceRequestDto ServiceRequestId { get; set; }
    public string? Address { get; set; }
}
