using job_portal_system.Data;
using job_portal_system.Hubs;
using job_portal_system.Models.Domain;
using job_portal_system.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace job_portal_system.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly UserManager<User> _userManager;

        public NotificationService(
       ApplicationDbContext context,
   IHubContext<NotificationHub> hubContext,
         UserManager<User> userManager)
        {
  _context = context;
         _hubContext = hubContext;
            _userManager = userManager;
        }

        public async Task CreateNotificationAsync(
    string userId,
            string title,
        string message,
  NotificationType type,
    string? relatedEntityId = null,
            string? actionUrl = null)
        {
Console.WriteLine($"[NotificationService] Creating notification for user: {userId}, Title: {title}");

         var notification = new Notification
         {
     UserId = userId,
                Title = title,
        Message = message,
       Type = type,
 RelatedEntityId = relatedEntityId,
         ActionUrl = actionUrl,
             IsRead = false,
                CreatedAt = DateTime.Now
       };

         _context.Notifications.Add(notification);
  await _context.SaveChangesAsync();

     Console.WriteLine($"[NotificationService] Notification saved to DB with ID: {notification.Id}");

       // Send real-time notification via SignalR
await SendNotificationToUserAsync(userId, notification);
        }

        public async Task CreateAdminNotificationAsync(
      string title,
            string message,
            NotificationType type,
          string? relatedEntityId = null,
            string? actionUrl = null)
        {
     // Get all admin users
            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            
      Console.WriteLine($"[NotificationService] Creating admin notification. Found {adminUsers.Count} admin users");

       foreach (var admin in adminUsers)
    {
                Console.WriteLine($"[NotificationService] Creating notification for admin: {admin.Email} (ID: {admin.Id})");
await CreateNotificationAsync(admin.Id, title, message, type, relatedEntityId, actionUrl);
 }
        }

        public async Task<List<Notification>> GetUserNotificationsAsync(string userId, int count = 10)
        {
    return await _context.Notifications
                .Where(n => n.UserId == userId)
          .OrderByDescending(n => n.CreatedAt)
          .Take(count)
   .ToListAsync();
  }

    public async Task<int> GetUnreadCountAsync(string userId)
        {
 return await _context.Notifications
     .Where(n => n.UserId == userId && !n.IsRead)
          .CountAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
{
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
    {
     notification.IsRead = true;
 await _context.SaveChangesAsync();
  }
        }

   public async Task MarkAllAsReadAsync(string userId)
        {
            var notifications = await _context.Notifications
      .Where(n => n.UserId == userId && !n.IsRead)
   .ToListAsync();

         foreach (var notification in notifications)
            {
     notification.IsRead = true;
            }

 await _context.SaveChangesAsync();
   }

        public async Task SendNotificationToUserAsync(string userId, Notification notification)
        {
        // Send to specific user via SignalR
      await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", new
            {
        notification.Id,
    notification.Title,
     notification.Message,
          notification.Type,
        notification.ActionUrl,
 notification.CreatedAt,
                notification.IsRead
            });
    }
    }
}
