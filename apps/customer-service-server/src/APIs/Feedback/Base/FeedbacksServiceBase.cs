using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using CustomerServiceManagement.APIs.Dtos;
using CustomerServiceManagement.APIs.Errors;
using CustomerServiceManagement.APIs.Extensions;
using CustomerServiceManagement.Infrastructure;
using CustomerServiceManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceManagement.APIs;

public abstract class FeedbacksServiceBase : IFeedbacksService
{
    protected readonly CustomerServiceManagementContext _context;

    public FeedbacksServiceBase(CustomerServiceManagementContext context)
    {
        _context = context;
    }

    private bool FeedbackExists(long id)
    {
        return _context.Feedbacks.Any(e => e.Id == id);
    }

    public async Task<FeedbackDto> CreateFeedback(FeedbackCreateInput inputDto)
    {
        var model = new Feedback { Id = inputDto.Id, Name = inputDto.Name, };
        _context.feedbacks.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Feedback>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task DeleteFeedback(string id)
    {
        var feedback = await _context.feedbacks.FindAsync(id);

        if (feedback == null)
        {
            throw new NotFoundException();
        }

        _context.feedbacks.Remove(feedback);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<FeedbackDto>> feedbacks(FeedbackFindMany findManyArgs)
    {
        var feedbacks = await _context
            .feedbacks.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return feedbacks.ConvertAll(feedback => feedback.ToDto());
    }

    public async Task UpdateFeedback(string id, FeedbackDto feedbackDto)
    {
        var feedback = new Feedback { Id = feedbackDto.Id, Name = feedbackDto.Name, };

        _context.Entry(feedback).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FeedbackExists(id))
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
