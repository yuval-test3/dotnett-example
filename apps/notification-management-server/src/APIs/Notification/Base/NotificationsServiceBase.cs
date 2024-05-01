using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using NotificationManagement.APIs.Dtos;
using NotificationManagement.APIs.Errors;
using NotificationManagement.APIs.Extensions;
using NotificationManagement.Infrastructure;
using NotificationManagement.Infrastructure.Models;

namespace NotificationManagement.APIs;

public abstract class NotificationsServiceBase : INotificationsService
{
    protected readonly NotificationManagementContext _context;

    public NotificationsServiceBase(NotificationManagementContext context)
    {
        _context = context;
    }

    private bool NotificationExists(long id)
    {
        return _context.Notifications.Any(e => e.Id == id);
    }

    public async Task<NotificationDto> CreateNotification(NotificationCreateInput inputDto)
    {
        var model = new Notification { Id = inputDto.Id, Name = inputDto.Name, };
        _context.notifications.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Notification>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task DeleteNotification(string id)
    {
        var notification = await _context.notifications.FindAsync(id);

        if (notification == null)
        {
            throw new NotFoundException();
        }

        _context.notifications.Remove(notification);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<NotificationDto>> notifications(NotificationFindMany findManyArgs)
    {
        var notifications = await _context
            .notifications.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return notifications.ConvertAll(notification => notification.ToDto());
    }

    public async Task UpdateNotification(string id, NotificationDto notificationDto)
    {
        var notification = new Notification
        {
            Id = notificationDto.Id,
            Name = notificationDto.Name,
        };

        _context.Entry(notification).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NotificationExists(id))
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
