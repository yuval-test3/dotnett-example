using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceManagement.Infrastructure.Models;

[Table("ServiceRequests")]
public class ServiceRequest
{
    [Key, Required]
    public long Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public OptionSet? Status { get; set; }

    public string ServiceTicketId { get; set; }

    [ForeignKey(nameof(ServiceTicketId))]
    public ServiceTicket? ServiceTicket { get; set; }

    public string? Description { get; set; }

    public string? Priority { get; set; }

    public ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public string? Title { get; set; }
}
