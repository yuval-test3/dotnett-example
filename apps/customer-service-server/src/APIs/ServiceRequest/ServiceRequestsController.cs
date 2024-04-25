using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceManagement.APIs;

[ApiController]
public class ServiceRequestsController : ServiceRequestsControllerBase
{
    public ServiceRequestsController(IServiceRequestsService service)
        : base(service) { }
}
