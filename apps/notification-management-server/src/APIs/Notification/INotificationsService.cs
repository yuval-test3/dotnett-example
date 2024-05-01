using System.ComponentModel.DataAnnotations;
using NotificationManagement.APIs.Dtos;

namespace NotificationManagement.APIs;

public interface INotificationsService
{
    public Task<Notification> CreateNotification(NotificationCreateInput input);
    public Task DeleteNotification(string id);
    public Task<IEnumerable<Notification>> Notifications();
    public Task UpdateNotification(string id, Notification dto);
}
