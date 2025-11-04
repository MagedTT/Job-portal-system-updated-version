using job_portal_system.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace job_portal_system.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
   private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
      _notificationService = notificationService;
}

        /// <summary>
        /// Simple test endpoint - access at /Notification/Test
 /// </summary>
  [HttpGet]
        public async Task<IActionResult> Test()
        {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.Identity?.Name ?? "Unknown";
         
  var notifications = await _notificationService.GetUserNotificationsAsync(userId ?? "", 10);
        var unreadCount = await _notificationService.GetUnreadCountAsync(userId ?? "");
            
       var result = new
       {
Status = "OK",
          UserId = userId,
       UserEmail = userEmail,
          UnreadCount = unreadCount,
          TotalNotifications = notifications.Count,
     Notifications = notifications.Select(n => new
  {
      n.Id,
           n.Title,
    n.Message,
  n.Type,
  n.IsRead,
        n.CreatedAt
})
    };
            
   return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
{
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
  if (string.IsNullOrEmpty(userId))
     return Unauthorized();

   var notifications = await _notificationService.GetUserNotificationsAsync(userId, 20);
      return Json(notifications);
        }

        [HttpGet]
  public async Task<IActionResult> GetUnreadCount()
    {
          var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
if (string.IsNullOrEmpty(userId))
       return Unauthorized();

   var count = await _notificationService.GetUnreadCountAsync(userId);
            return Json(new { count });
   }

        [HttpPost]
  public async Task<IActionResult> MarkAsRead(int id)
     {
            await _notificationService.MarkAsReadAsync(id);
   return Ok();
 }

 [HttpPost]
        public async Task<IActionResult> MarkAllAsRead()
        {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
          if (string.IsNullOrEmpty(userId))
        return Unauthorized();

    await _notificationService.MarkAllAsReadAsync(userId);
            return Ok();
        }

        public async Task<IActionResult> Index()
        {
     var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

    var notifications = await _notificationService.GetUserNotificationsAsync(userId, 50);
  return View(notifications);
     }
    }
}
