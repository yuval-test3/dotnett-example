namespace CustomerServiceManagement.APIs.Dtos;

public class ServiceRequestCreateInput
{
    public DateTime CreatedAt { get; set; }
    public ServiceTicketDto ServiceTicketId { get; set; }
    public string? Priority { get; set; }
    public ICollection<CustomerDto>? Customers { get; set; }
    public string? Title { get; set; }
}
