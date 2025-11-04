using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace job_portal_system.Hubs
{
  [Authorize]
 public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
         var userId = Context.UserIdentifier;
          
     if (!string.IsNullOrEmpty(userId))
   {
        // User connected, you can add them to groups if needed
          await Groups.AddToGroupAsync(Context.ConnectionId, userId);
       }
            
   await base.OnConnectedAsync();
      }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
    
            if (!string.IsNullOrEmpty(userId))
 {
  await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
          }
            
            await base.OnDisconnectedAsync(exception);
        }
    }
}
