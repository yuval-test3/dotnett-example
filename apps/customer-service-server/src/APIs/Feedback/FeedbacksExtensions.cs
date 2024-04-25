using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.Infrastructure.Models;

namespace CustomerServiceManagement.APIs.Extensions;

public static class FeedbacksExtensions
{
    public static FeedbackDto ToDto(this Feedback model)
    {
        return new FeedbackDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Content = model.Content,
            Rating = model.Rating,
            Customers = model.Customers.Select(x => x.ToDto()).ToList(),
        };
    }
}
