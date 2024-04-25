using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceManagement.APIs;

[ApiController]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
