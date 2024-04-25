using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.APIs.Errors;
using CustomerServiceManagement.APIs.Extensions;
using CustomerServiceManagement.Infrastructure;
using CustomerServiceManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceManagement.APIs;

public abstract class CustomersServiceBase : ICustomersService
{
    protected readonly CustomerServiceManagementContext _context;

    public CustomersServiceBase(CustomerServiceManagementContext context)
    {
        _context = context;
    }

    private bool CustomerExists(long id)
    {
        return _context.Customers.Any(e => e.Id == id);
    }

    public async Task<CustomerDto> CreateCustomer(CustomerCreateInput inputDto)
    {
        var model = new Customer { Id = inputDto.Id, Name = inputDto.Name, };
        _context.customers.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Customer>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task ConnectFeedback(string id, [Required] string feedbackId)
    {
        var customer = await _context.customers.FindAsync(id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var feedback = await _context.feedbacks.FindAsync(feedbackId);
        if (feedback == null)
        {
            throw new NotFoundException();
        }

        customer.feedbacks.Add(feedback);
        await _context.SaveChangesAsync();
    }

    public async Task ConnectServiceRequest(string id, [Required] string serviceRequestId)
    {
        var customer = await _context.customers.FindAsync(id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var serviceRequest = await _context.serviceRequests.FindAsync(serviceRequestId);
        if (serviceRequest == null)
        {
            throw new NotFoundException();
        }

        customer.serviceRequests.Add(serviceRequest);
        await _context.SaveChangesAsync();
    }

    public async Task DisconnectFeedback(string id, [Required] string feedbackId)
    {
        var customer = await _context.customers.FindAsync(id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var feedback = await _context.feedbacks.FindAsync(feedbackId);
        if (feedback == null)
        {
            throw new NotFoundException();
        }

        customer.feedbacks.Remove(feedback);
        await _context.SaveChangesAsync();
    }

    public async Task DisconnectServiceRequest(string id, [Required] string serviceRequestId)
    {
        var customer = await _context.customers.FindAsync(id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var serviceRequest = await _context.serviceRequests.FindAsync(serviceRequestId);
        if (serviceRequest == null)
        {
            throw new NotFoundException();
        }

        customer.serviceRequests.Remove(serviceRequest);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<FeedbackDto>> Feedbacks(string id)
    {
        var customer = await _context.customers.FindAsync(id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        return customer.Feedbacks.Select(feedback => feedback.ToDto()).ToList();
    }

    public async Task<IEnumerable<ServiceRequestDto>> ServiceRequests(string id)
    {
        var customer = await _context.customers.FindAsync(id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        return customer.ServiceRequests.Select(serviceRequest => serviceRequest.ToDto()).ToList();
    }

    public async Task UpdateFeedbacks(CustomerIdDto idDto, FeedbackIdDto[] feedbacksId)
    {
        var customer = await _context
            .customers.Include(x => x.Feedbacks)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var feedbacks = await _context
            .Feedbacks.Where(t => feedbacksId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (feedbacks.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Feedbacks = feedbacks;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateServiceRequests(
        CustomerIdDto idDto,
        ServiceRequestIdDto[] serviceRequestsId
    )
    {
        var customer = await _context
            .customers.Include(x => x.ServiceRequests)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (customer == null)
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

        customer.ServiceRequests = serviceRequests;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCustomer(string id)
    {
        var customer = await _context.customers.FindAsync(id);

        if (customer == null)
        {
            throw new NotFoundException();
        }

        _context.customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CustomerDto>> customers(CustomerFindMany findManyArgs)
    {
        var customers = await _context
            .customers.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return customers.ConvertAll(customer => customer.ToDto());
    }

    public async Task UpdateCustomer(string id, CustomerDto customerDto)
    {
        var customer = new Customer { Id = customerDto.Id, Name = customerDto.Name, };

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CustomerExists(id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
