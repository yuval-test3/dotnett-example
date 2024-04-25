using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.Infrastructure.Models;

namespace CustomerServiceManagement.APIs.Extensions;

public static class ServiceTicketsExtensions
{
    public static ServiceTicketDto ToDto(this ServiceTicket model)
    {
        return new ServiceTicketDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Status = model.Status,
            Issue = model.Issue,
            Resolution = model.Resolution,
            ServiceRequests = model.ServiceRequests.Select(x => x.ToDto()).ToList(),
        };
    }
}
