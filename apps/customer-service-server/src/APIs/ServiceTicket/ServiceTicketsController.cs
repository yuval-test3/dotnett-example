using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceManagement.APIs;

[ApiController]
public class ServiceTicketsController : ServiceTicketsControllerBase
{
    public ServiceTicketsController(IServiceTicketsService service)
        : base(service) { }
}
