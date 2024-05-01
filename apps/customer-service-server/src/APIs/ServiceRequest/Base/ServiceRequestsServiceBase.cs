using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.APIs.Errors;
using CustomerServiceManagement.APIs.Extensions;
using CustomerServiceManagement.Infrastructure;
using CustomerServiceManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceManagement.APIs;

public abstract class ServiceRequestsServiceBase : IServiceRequestsService
{
    protected readonly CustomerServiceManagementContext _context;

    public ServiceRequestsServiceBase(CustomerServiceManagementContext context)
    {
        _context = context;
    }

    private bool ServiceRequestExists(long id)
    {
        return _context.ServiceRequests.Any(e => e.Id == id);
    }

    public async Task UpdateServiceRequest(string id, ServiceRequestDto serviceRequestDto)
    {
        var serviceRequest = new ServiceRequest
        {
            Id = serviceRequestDto.Id,
            Name = serviceRequestDto.Name,
        };

        _context.Entry(serviceRequest).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ServiceRequestExists(id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    public async Task UpdateServiceTickets(
        ServiceRequestIdDto idDto,
        ServiceTicketIdDto[] serviceTicketsId
    )
    {
        var serviceRequest = await _context
            .serviceRequests.Include(x => x.ServiceTickets)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (serviceRequest == null)
        {
            throw new NotFoundException();
        }

        var serviceTickets = await _context
            .ServiceTickets.Where(t => serviceTicketsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (serviceTickets.Count == 0)
        {
            throw new NotFoundException();
        }

        serviceRequest.ServiceTickets = serviceTickets;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCustomers(ServiceRequestIdDto idDto, CustomerIdDto[] customersId)
    {
        var serviceRequest = await _context
            .serviceRequests.Include(x => x.Customers)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (serviceRequest == null)
        {
            throw new NotFoundException();
        }

        var customers = await _context
            .Customers.Where(t => customersId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (customers.Count == 0)
        {
            throw new NotFoundException();
        }

        serviceRequest.Customers = customers;
        await _context.SaveChangesAsync();
    }

    public async Task<ServiceRequestDto> CreateServiceRequest(ServiceRequestCreateInput inputDto)
    {
        var model = new ServiceRequest { Id = inputDto.Id, Name = inputDto.Name, };
        _context.serviceRequests.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ServiceRequest>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task DisconnectServiceTicket(string id, [Required] string serviceTicketId)
    {
        var serviceRequest = await _context.serviceRequests.FindAsync(id);
        if (serviceRequest == null)
        {
            throw new NotFoundException();
        }

        var serviceTicket = await _context.serviceTickets.FindAsync(serviceTicketId);
        if (serviceTicket == null)
        {
            throw new NotFoundException();
        }

        serviceRequest.serviceTickets.Remove(serviceTicket);
        await _context.SaveChangesAsync();
    }

    public async Task DisconnectCustomer(string id, [Required] string customerId)
    {
        var serviceRequest = await _context.serviceRequests.FindAsync(id);
        if (serviceRequest == null)
        {
            throw new NotFoundException();
        }

        var customer = await _context.customers.FindAsync(customerId);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        serviceRequest.customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ServiceRequestDto>> serviceRequests(
        ServiceRequestFindMany findManyArgs
    )
    {
        var serviceRequests = await _context
            .serviceRequests.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return serviceRequests.ConvertAll(serviceRequest => serviceRequest.ToDto());
    }

    public async Task DeleteServiceRequest(string id)
    {
        var serviceRequest = await _context.serviceRequests.FindAsync(id);

        if (serviceRequest == null)
        {
            throw new NotFoundException();
        }

        _context.serviceRequests.Remove(serviceRequest);
        await _context.SaveChangesAsync();
    }

    public async Task ConnectServiceTicket(string id, [Required] string serviceTicketId)
    {
        var serviceRequest = await _context.serviceRequests.FindAsync(id);
        if (serviceRequest == null)
        {
            throw new NotFoundException();
        }

        var serviceTicket = await _context.serviceTickets.FindAsync(serviceTicketId);
        if (serviceTicket == null)
        {
            throw new NotFoundException();
        }

        serviceRequest.serviceTickets.Add(serviceTicket);
        await _context.SaveChangesAsync();
    }

    public async Task ConnectCustomer(string id, [Required] string customerId)
    {
        var serviceRequest = await _context.serviceRequests.FindAsync(id);
        if (serviceRequest == null)
        {
            throw new NotFoundException();
        }

        var customer = await _context.customers.FindAsync(customerId);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        serviceRequest.customers.Add(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ServiceTicketDto>> ServiceTickets(string id)
    {
        var serviceRequest = await _context.serviceRequests.FindAsync(id);
        if (serviceRequest == null)
        {
            throw new NotFoundException();
        }

        return serviceRequest
            .ServiceTickets.Select(serviceTicket => serviceTicket.ToDto())
            .ToList();
    }

    public async Task<IEnumerable<CustomerDto>> Customers(string id)
    {
        var serviceRequest = await _context.serviceRequests.FindAsync(id);
        if (serviceRequest == null)
        {
            throw new NotFoundException();
        }

        return serviceRequest.Customers.Select(customer => customer.ToDto()).ToList();
    }
}
