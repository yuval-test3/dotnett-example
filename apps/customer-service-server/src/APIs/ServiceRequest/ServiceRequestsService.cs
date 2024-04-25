using CustomerServiceManagement.Infrastructure;

namespace CustomerServiceManagement.APIs;

public class ServiceRequestsService : ServiceRequestsServiceBase
{
    public ServiceRequestsService(CustomerServiceManagementContext context)
        : base(context) { }
}
