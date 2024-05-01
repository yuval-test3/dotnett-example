using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceManagement.Infrastructure.Models;

[Table("Customers")]
public class Customer
{
    [Key, Required]
    public long Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public OptionSet? Status { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Name { get; set; }

    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public string ServiceRequestId { get; set; }

    [ForeignKey(nameof(ServiceRequestId))]
    public ServiceRequest? ServiceRequest { get; set; }

    public string? Address { get; set; }

    public string? NewField { get; set; }

    public string? Nn { get; set; }

    public string? Phone_2 { get; set; }
}
