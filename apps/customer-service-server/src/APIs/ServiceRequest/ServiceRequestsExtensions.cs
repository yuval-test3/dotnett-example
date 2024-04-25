using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.Infrastructure.Models;

namespace CustomerServiceManagement.APIs.Extensions;

public static class ServiceRequestsExtensions
{
    public static ServiceRequestDto ToDto(this ServiceRequest model)
    {
        return new ServiceRequestDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Status = model.Status,
            ServiceTickets = model.ServiceTickets.Select(x => x.ToDto()).ToList(),
            Description = model.Description,
            Priority = model.Priority,
            Customers = model.Customers.Select(x => x.ToDto()).ToList(),
            Title = model.Title,
        };
    }
}
