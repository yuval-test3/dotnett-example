using Microsoft.AspNetCore.Mvc;

namespace NotificationManagement.APIs;

[ApiController]
public class LogsController : LogsControllerBase
{
    public LogsController(ILogsService service)
        : base(service) { }
}
