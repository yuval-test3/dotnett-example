using Microsoft.AspNetCore.Mvc;

namespace NotificationManagement.APIs;

[ApiController]
public class NotificationsController : NotificationsControllerBase
{
    public NotificationsController(INotificationsService service)
        : base(service) { }
}
