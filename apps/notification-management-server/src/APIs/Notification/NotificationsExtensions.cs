using NotificationManagement.APIs.Dtos;
using NotificationManagement.Infrastructure.Models;

namespace NotificationManagement.APIs.Extensions;

public static class NotificationsExtensions
{
    public static NotificationDto ToDto(this Notification model)
    {
        return new NotificationDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Status = model.Status,
            Message = model.Message,
            Recipient = model.Recipient,
            Title = model.Title,
        };
    }
}
