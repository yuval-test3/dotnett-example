using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using NotificationManagement.APIs.Dtos;
using NotificationManagement.APIs.Errors;
using NotificationManagement.APIs.Extensions;
using NotificationManagement.Infrastructure;
using NotificationManagement.Infrastructure.Models;

namespace NotificationManagement.APIs;

public abstract class LogsServiceBase : ILogsService
{
    protected readonly NotificationManagementContext _context;

    public LogsServiceBase(NotificationManagementContext context)
    {
        _context = context;
    }

    private bool LogExists(long id)
    {
        return _context.Logs.Any(e => e.Id == id);
    }

    public async Task<LogDto> CreateLog(LogCreateInput inputDto)
    {
        var model = new Log { Id = inputDto.Id, Name = inputDto.Name, };
        _context.logs.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Log>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task DeleteLog(string id)
    {
        var log = await _context.logs.FindAsync(id);

        if (log == null)
        {
            throw new NotFoundException();
        }

        _context.logs.Remove(log);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<LogDto>> logs(LogFindMany findManyArgs)
    {
        var logs = await _context
            .logs.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return logs.ConvertAll(log => log.ToDto());
    }

    public async Task UpdateLog(string id, LogDto logDto)
    {
        var log = new Log { Id = logDto.Id, Name = logDto.Name, };

        _context.Entry(log).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LogExists(id))
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
