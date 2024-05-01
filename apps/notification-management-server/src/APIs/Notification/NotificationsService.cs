using NotificationManagement.Infrastructure;

namespace NotificationManagement.APIs;

public class NotificationsService : NotificationsServiceBase
{
    public NotificationsService(NotificationManagementContext context)
        : base(context) { }
}
