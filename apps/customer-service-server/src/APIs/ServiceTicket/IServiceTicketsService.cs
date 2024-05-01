using System.ComponentModel.DataAnnotations;
using CustomerServiceManagement.APIs.Dtos;

namespace CustomerServiceManagement.APIs;

public interface IServiceTicketsService
{
    public Task DisconnectServiceRequest(string id, [Required] string ServiceRequestId);
    public Task UpdateServiceRequests(
        ServiceTicketIdDto idDto,
        ServiceRequestIdDto[] ServiceRequestsId
    );
    public Task ConnectServiceRequest(string id, [Required] string ServiceRequestId);
    public Task UpdateServiceTicket(string id, ServiceTicket dto);
    public Task<IEnumerable<ServiceTicket>> ServiceTickets();
    public Task<IEnumerable<ServiceRequest>> ServiceRequests(string id);
    public Task<ServiceTicket> CreateServiceTicket(ServiceTicketCreateInput input);
    public Task DeleteServiceTicket(string id);
}
