using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.Infrastructure.Models;

namespace CustomerServiceManagement.APIs.Extensions;

public static class CustomersExtensions
{
    public static CustomerDto ToDto(this Customer model)
    {
        return new CustomerDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Status = model.Status,
            Email = model.Email,
            Phone = model.Phone,
            Name = model.Name,
            Feedbacks = model.Feedbacks.Select(x => x.ToDto()).ToList(),
            ServiceRequests = model.ServiceRequests.Select(x => x.ToDto()).ToList(),
            Address = model.Address,
            NewField = model.NewField,
            Nn = model.Nn,
        };
    }
}
