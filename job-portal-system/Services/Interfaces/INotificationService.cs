using job_portal_system.Models.Domain;

namespace job_portal_system.Services.Interfaces
{
    public interface INotificationService
 {
        Task CreateNotificationAsync(string userId, string title, string message, NotificationType type, string? relatedEntityId = null, string? actionUrl = null);
    Task CreateAdminNotificationAsync(string title, string message, NotificationType type, string? relatedEntityId = null, string? actionUrl = null);
        Task<List<Notification>> GetUserNotificationsAsync(string userId, int count = 10);
      Task<int> GetUnreadCountAsync(string userId);
 Task MarkAsReadAsync(int notificationId);
        Task MarkAllAsReadAsync(string userId);
 Task SendNotificationToUserAsync(string userId, Notification notification);
    }
}
