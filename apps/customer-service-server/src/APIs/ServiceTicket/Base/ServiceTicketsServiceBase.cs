using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.APIs.Errors;
using CustomerServiceManagement.APIs.Extensions;
using CustomerServiceManagement.Infrastructure;
using CustomerServiceManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceManagement.APIs;

public abstract class ServiceTicketsServiceBase : IServiceTicketsService
{
    protected readonly CustomerServiceManagementContext _context;

    public ServiceTicketsServiceBase(CustomerServiceManagementContext context)
    {
        _context = context;
    }

    private bool ServiceTicketExists(long id)
    {
        return _context.ServiceTickets.Any(e => e.Id == id);
    }

    public async Task DisconnectServiceRequest(string id, [Required] string serviceRequestId)
    {
        var serviceTicket = await _context.serviceTickets.FindAsync(id);
        if (serviceTicket == null)
        {
            throw new NotFoundException();
        }

        var serviceRequest = await _context.serviceRequests.FindAsync(serviceRequestId);
        if (serviceRequest == null)
        {
            throw new NotFoundException();
        }

        serviceTicket.serviceRequests.Remove(serviceRequest);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateServiceRequests(
        ServiceTicketIdDto idDto,
        ServiceRequestIdDto[] serviceRequestsId
    )
    {
        var serviceTicket = await _context
            .serviceTickets.Include(x => x.ServiceRequests)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (serviceTicket == null)
        {
            throw new NotFoundException();
        }

        var serviceRequests = await _context
            .ServiceRequests.Where(t => serviceRequestsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (serviceRequests.Count == 0)
        {
            throw new NotFoundException();
        }

        serviceTicket.ServiceRequests = serviceRequests;
        await _context.SaveChangesAsync();
    }

    public async Task ConnectServiceRequest(string id, [Required] string serviceRequestId)
    {
        var serviceTicket = await _context.serviceTickets.FindAsync(id);
        if (serviceTicket == null)
        {
            throw new NotFoundException();
        }

        var serviceRequest = await _context.serviceRequests.FindAsync(serviceRequestId);
        if (serviceRequest == null)
        {
            throw new NotFoundException();
        }

        serviceTicket.serviceRequests.Add(serviceRequest);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateServiceTicket(string id, ServiceTicketDto serviceTicketDto)
    {
        var serviceTicket = new ServiceTicket
        {
            Id = serviceTicketDto.Id,
            Name = serviceTicketDto.Name,
        };

        _context.Entry(serviceTicket).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ServiceTicketExists(id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<IEnumerable<ServiceTicketDto>> serviceTickets(
        ServiceTicketFindMany findManyArgs
    )
    {
        var serviceTickets = await _context
            .serviceTickets.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return serviceTickets.ConvertAll(serviceTicket => serviceTicket.ToDto());
    }

    public async Task<IEnumerable<ServiceRequestDto>> ServiceRequests(string id)
    {
        var serviceTicket = await _context.serviceTickets.FindAsync(id);
        if (serviceTicket == null)
        {
            throw new NotFoundException();
        }

        return serviceTicket
            .ServiceRequests.Select(serviceRequest => serviceRequest.ToDto())
            .ToList();
    }

    public async Task<ServiceTicketDto> CreateServiceTicket(ServiceTicketCreateInput inputDto)
    {
        var model = new ServiceTicket { Id = inputDto.Id, Name = inputDto.Name, };
        _context.serviceTickets.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ServiceTicket>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task DeleteServiceTicket(string id)
    {
        var serviceTicket = await _context.serviceTickets.FindAsync(id);

        if (serviceTicket == null)
        {
            throw new NotFoundException();
        }

        _context.serviceTickets.Remove(serviceTicket);
        await _context.SaveChangesAsync();
    }
}
