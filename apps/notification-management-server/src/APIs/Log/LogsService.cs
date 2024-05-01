using NotificationManagement.Infrastructure;

namespace NotificationManagement.APIs;

public class LogsService : LogsServiceBase
{
    public LogsService(NotificationManagementContext context)
        : base(context) { }
}
