using job_portal_system.Data;
using job_portal_system.Models.Domain;
using job_portal_system.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace job_portal_system.Controllers
{
    [Authorize]
  public class DebugNotificationController : Controller
    {
     private readonly INotificationService _notificationService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

  public DebugNotificationController(
      INotificationService notificationService,
            ApplicationDbContext context,
      UserManager<User> userManager)
        {
 _notificationService = notificationService;
            _context = context;
          _userManager = userManager;
        }

    /// <summary>
    /// Test endpoint to manually create notifications and check database
        /// Access: /DebugNotification/TestNotifications
        /// </summary>
      public async Task<IActionResult> TestNotifications()
        {
            var results = new List<string>();

            try
          {
           // 1. Check current user
       var currentUser = await _userManager.GetUserAsync(User);
     results.Add($"? Current User: {currentUser?.Email} (ID: {currentUser?.Id})");

     // 2. Check admin users
  var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
     results.Add($"? Found {adminUsers.Count} Admin users:");
            foreach (var admin in adminUsers)
                {
             results.Add($"   - {admin.Email} (ID: {admin.Id})");
       }

      // 3. Check all notifications in database
        var allNotifications = await _context.Notifications
           .Include(n => n.User)
  .OrderByDescending(n => n.CreatedAt)
   .Take(20)
               .ToListAsync();

    results.Add($"? Total notifications in DB: {allNotifications.Count}");
    foreach (var notif in allNotifications.Take(5))
      {
       results.Add($"   - User: {notif.User.Email}, Title: {notif.Title}, Read: {notif.IsRead}");
                }

   // 4. Test creating a notification for current user
     if (currentUser != null)
    {
    await _notificationService.CreateNotificationAsync(
       userId: currentUser.Id,
         title: "Test Notification",
          message: "This is a test notification created at " + DateTime.Now.ToString("HH:mm:ss"),
           type: NotificationType.NewUser,
     actionUrl: "/Home/Index"
         );
 results.Add("? Created test notification for current user");
         }

      // 5. Test creating admin notification
       await _notificationService.CreateAdminNotificationAsync(
      title: "Test Admin Notification",
         message: "This is a test admin notification created at " + DateTime.Now.ToString("HH:mm:ss"),
           type: NotificationType.NewUser,
        actionUrl: "/Admin/Dashboard"
         );
    results.Add("? Created test admin notification");

       // 6. Check notifications after creation
         var userNotifications = await _context.Notifications
    .Where(n => n.UserId == currentUser!.Id)
.OrderByDescending(n => n.CreatedAt)
  .Take(5)
  .ToListAsync();

          results.Add($"? Current user has {userNotifications.Count} notifications");

      // 7. Get unread count
           var unreadCount = await _notificationService.GetUnreadCountAsync(currentUser!.Id);
       results.Add($"? Unread count for current user: {unreadCount}");

        }
      catch (Exception ex)
      {
   results.Add($"? ERROR: {ex.Message}");
          results.Add($"   Stack: {ex.StackTrace}");
       }

            ViewBag.Results = results;
            return View();
        }

        /// <summary>
    /// Check database state
      /// Access: /DebugNotification/CheckDatabase
        /// </summary>
        public async Task<IActionResult> CheckDatabase()
        {
         var results = new Dictionary<string, object>();

     try
  {
    // Check notifications count
       var notificationCount = await _context.Notifications.CountAsync();
            results["Total Notifications"] = notificationCount;

    // Check users
           var userCount = await _context.Users.CountAsync();
        results["Total Users"] = userCount;

          // Check roles
       var roles = await _context.Roles.ToListAsync();
    results["Roles"] = string.Join(", ", roles.Select(r => r.Name));

      // Check admin role members
  var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
                if (adminRole != null)
     {
          var adminUserRoles = await _context.UserRoles
    .Where(ur => ur.RoleId == adminRole.Id)
          .ToListAsync();
         results["Admin Users Count"] = adminUserRoles.Count;
     }

         // Get sample notifications
     var sampleNotifications = await _context.Notifications
.Include(n => n.User)
      .OrderByDescending(n => n.CreatedAt)
             .Take(10)
          .Select(n => new
      {
        n.Id,
 UserEmail = n.User.Email,
               n.Title,
      n.Message,
  n.Type,
      n.IsRead,
         n.CreatedAt
    })
  .ToListAsync();

 results["Sample Notifications"] = sampleNotifications;

           // Check employer and jobseeker counts
                results["Employers"] = await _context.Employers.CountAsync();
  results["JobSeekers"] = await _context.JobSeekers.CountAsync();
          results["Jobs"] = await _context.Jobs.CountAsync();
   results["Applications"] = await _context.Applications.CountAsync();

 }
            catch (Exception ex)
{
      results["Error"] = ex.Message;
   }

          return Json(results);
        }
}
}
