using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.Infrastructure.Models;

namespace CustomerServiceManagement.APIs.Extensions;

public static class OrdersExtensions
{
    public static OrderDto ToDto(this Order model)
    {
        return new OrderDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Status = model.Status,
            TotalAmount = model.TotalAmount,
            OrderNumber = model.OrderNumber,
            PlacedAt = model.PlacedAt,
            Products = model.Products.Select(x => x.ToDto()).ToList(),
        };
    }
}
