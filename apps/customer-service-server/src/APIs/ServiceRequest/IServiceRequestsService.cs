using System.ComponentModel.DataAnnotations;
using CustomerServiceManagement.APIs.Dtos;

namespace CustomerServiceManagement.APIs;

public interface IServiceRequestsService
{
    public Task UpdateServiceRequest(string id, ServiceRequest dto);
    public Task UpdateServiceTickets(
        ServiceRequestIdDto idDto,
        ServiceTicketIdDto[] ServiceTicketsId
    );
    public Task UpdateCustomers(ServiceRequestIdDto idDto, CustomerIdDto[] CustomersId);
    public Task<ServiceRequest> CreateServiceRequest(ServiceRequestCreateInput input);
    public Task DisconnectServiceTicket(string id, [Required] string ServiceTicketId);
    public Task DisconnectCustomer(string id, [Required] string CustomerId);
    public Task<IEnumerable<ServiceRequest>> ServiceRequests();
    public Task DeleteServiceRequest(string id);
    public Task ConnectServiceTicket(string id, [Required] string ServiceTicketId);
    public Task ConnectCustomer(string id, [Required] string CustomerId);
    public Task<IEnumerable<ServiceTicket>> ServiceTickets(string id);
    public Task<IEnumerable<Customer>> Customers(string id);
    public Task<IEnumerable<ServiceTicket>> ServiceTickets(string id);
    public Task<IEnumerable<Customer>> Customers(string id);
}
