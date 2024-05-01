using NotificationManagement.APIs.Dtos;
using NotificationManagement.Infrastructure.Models;

namespace NotificationManagement.APIs.Extensions;

public static class LogsExtensions
{
    public static LogDto ToDto(this Log model)
    {
        return new LogDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Level = model.Level,
            Timestamp = model.Timestamp,
            Message = model.Message,
        };
    }
}
