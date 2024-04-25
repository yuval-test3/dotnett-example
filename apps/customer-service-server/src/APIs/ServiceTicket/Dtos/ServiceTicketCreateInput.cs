namespace CustomerServiceManagement.APIs.Dtos;

public class ServiceTicketCreateInput
{
    public DateTime CreatedAt { get; set; }
    public bool? Issue { get; set; }
    public string? Resolution { get; set; }
    public ICollection<ServiceRequestDto>? ServiceRequests { get; set; }
}
