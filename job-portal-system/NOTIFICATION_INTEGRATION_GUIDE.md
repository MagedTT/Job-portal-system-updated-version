# Notification System Integration Guide

This document explains how to integrate notifications into your existing controllers.

## Setup Complete ?

The notification system is now fully set up with:
- ? Notification model with NotificationType enum
- ? SignalR Hub for real-time notifications
- ? Notification Service for creating and managing notifications
- ? Notification Controller with API endpoints
- ? UI integration in _Layout.cshtml with notification bell
- ? SignalR client JavaScript for real-time updates
- ? Database migration applied

## How to Use Notifications in Your Controllers

### Step 1: Inject INotificationService

Add the notification service to your controller constructor:

```csharp
private readonly INotificationService _notificationService;

public YourController(INotificationService notificationService)
{
    _notificationService = notificationService;
}
```

### Step 2: Create Notifications

Use these examples to add notifications in your existing controllers:

---

## Integration Points

### 1. **Admin Notifications** (New Users, New Jobs, New Applications)

#### In `AuthService.cs` - After JobSeeker Registration
```csharp
// In RegisterJobSeekerAsync method, after successful registration
await _notificationService.CreateAdminNotificationAsync(
    title: "New Job Seeker Registered",
    message: $"{jobSeeker.FirstName} {jobSeeker.LastName} has registered as a job seeker",
    type: NotificationType.NewUser,
    actionUrl: "/Admin/ManageUsers"
);
```

#### In `AuthService.cs` - After Employer Registration
```csharp
// In RegisterEmployerAsync method, after successful registration
await _notificationService.CreateAdminNotificationAsync(
    title: "New Employer Registered",
    message: $"{employer.CompanyName} has registered as an employer",
    type: NotificationType.NewUser,
    actionUrl: "/Admin/ManageUsers"
);
```

#### In `JobController.cs` - After Job Creation
```csharp
// In Create action, after job is created
await _notificationService.CreateAdminNotificationAsync(
    title: "New Job Posted",
    message: $"New job '{job.Title}' posted by {employer.CompanyName}",
    type: NotificationType.NewJobPost,
    relatedEntityId: job.Id.ToString(),
    actionUrl: $"/Admin/ManageJobs"
);
```

#### In `JobController.cs` or `ApplicationController.cs` - After Job Application
```csharp
// In Apply action, after application is created
await _notificationService.CreateAdminNotificationAsync(
    title: "New Job Application",
    message: $"{jobSeeker.FirstName} {jobSeeker.LastName} applied to '{job.Title}'",
    type: NotificationType.NewApplication,
    relatedEntityId: application.Id.ToString(),
  actionUrl: $"/Admin/ManageJobs"
);
```

---

### 2. **Employer Notifications** (Job Applications, Withdrawals)

#### In `JobController.cs` or `ApplicationController.cs` - After Job Application Received
```csharp
// In Apply action, after creating application
// Get employer user ID
var employer = await _context.Employers
    .Include(e => e.User)
    .FirstOrDefaultAsync(e => e.Id == job.EmployerId);

if (employer != null)
{
    await _notificationService.CreateNotificationAsync(
      userId: employer.UserId,
        title: "New Application Received",
    message: $"{jobSeeker.FirstName} {jobSeeker.LastName} applied to your job '{job.Title}'",
    type: NotificationType.JobApplicationReceived,
  relatedEntityId: application.Id.ToString(),
        actionUrl: $"/Employer/Applicants?jobId={job.Id}"
    );
}
```

#### In `ApplicationController.cs` - After Application Withdrawal
```csharp
// In WithdrawApplication action
var job = await _context.Jobs
.Include(j => j.Employer)
.FirstOrDefaultAsync(j => j.Id == application.JobId);

if (job?.Employer != null)
{
await _notificationService.CreateNotificationAsync(
        userId: job.Employer.UserId,
        title: "Application Withdrawn",
        message: $"An application for '{job.Title}' has been withdrawn",
        type: NotificationType.ApplicationWithdrawn,
        relatedEntityId: job.Id.ToString(),
        actionUrl: "/Employer/Applicants"
    );
}
```

---

### 3. **Job Seeker Notifications** (New Jobs, Application Status Changes)

#### In `JobController.cs` - After New Job Posted (Notify All Job Seekers)
```csharp
// In Create action, after job is successfully created
// Notify all active job seekers
var jobSeekers = await _context.JobSeekers
    .Where(js => js.User.IsActive)
    .ToListAsync();

foreach (var js in jobSeekers)
{
    await _notificationService.CreateNotificationAsync(
        userId: js.UserId,
        title: "New Job Posted",
    message: $"A new job '{job.Title}' has been posted by {employer.CompanyName}",
        type: NotificationType.NewJobPosted,
        relatedEntityId: job.Id.ToString(),
actionUrl: $"/Job/Details/{job.Id}"
    );
}
```

#### In `EmployerController.cs` - After Application Status Change (Accept)
```csharp
// In UpdateApplicationStatus action when accepting
var application = await _context.Applications
    .Include(a => a.JobSeeker)
    .Include(a => a.Job)
.FirstOrDefaultAsync(a => a.Id == id);

if (application != null && status == Status.Accepted)
{
 await _notificationService.CreateNotificationAsync(
userId: application.JobSeeker.UserId,
      title: "Application Accepted! ??",
        message: $"Congratulations! Your application for '{application.Job.Title}' has been accepted",
  type: NotificationType.ApplicationAccepted,
        relatedEntityId: application.Id.ToString(),
 actionUrl: $"/Application/Details/{application.Id}"
    );
}
```

#### In `EmployerController.cs` - After Application Status Change (Reject)
```csharp
// In UpdateApplicationStatus action when rejecting
if (application != null && status == Status.Rejected)
{
    await _notificationService.CreateNotificationAsync(
        userId: application.JobSeeker.UserId,
    title: "Application Status Updated",
        message: $"Your application for '{application.Job.Title}' has been reviewed",
    type: NotificationType.ApplicationRejected,
        relatedEntityId: application.Id.ToString(),
        actionUrl: $"/Application/Details/{application.Id}"
    );
}
```

---

## Quick Reference - NotificationType Enum

```csharp
public enum NotificationType
{
    // Admin notifications
    NewUser,
    NewJobPost,
    NewApplication,
    
    // Employer notifications
    JobApplicationReceived,
    ApplicationWithdrawn,
    
    // JobSeeker notifications
    NewJobPosted,
    ApplicationStatusChanged,
    ApplicationAccepted,
    ApplicationRejected
}
```

---

## Testing the Notification System

1. **Register a new user** - Admin should receive a notification
2. **Create a new job** - Admin and all job seekers should receive notifications
3. **Apply to a job** - Employer and admin should receive notifications
4. **Accept/Reject application** - Job seeker should receive notification
5. **Withdraw application** - Employer should receive notification

---

## Notification Features

- ? Real-time notifications via SignalR
- ? Notification badge with unread count
- ? Notification dropdown in header
- ? Full notification page at `/Notification/Index`
- ? Mark individual notifications as read
- ? Mark all notifications as read
- ? Browser push notifications (with permission)
- ? Clickable notifications with action URLs
- ? Icon-based notification types
- ? Responsive design

---

## Next Steps

1. Add the notification code snippets above to your existing controllers
2. Test each notification type
3. Customize notification messages as needed
4. (Optional) Add email notifications alongside in-app notifications
5. (Optional) Add notification preferences per user

---

## Files Modified/Created

### Created:
- `Models/Domain/Notification.cs` - Enhanced with NotificationType
- `Hubs/NotificationHub.cs` - SignalR hub
- `Services/Interfaces/INotificationService.cs`
- `Services/Implementations/NotificationService.cs`
- `Controllers/NotificationController.cs`
- `Views/Notification/Index.cshtml`
- `Data/Configurations/NotificationConfiguration.cs`

### Modified:
- `Program.cs` - Added SignalR and NotificationService
- `Views/Shared/_Layout.cshtml` - Added notification bell and SignalR client
- Database - Migration for enhanced Notification model

---

## Support

For issues or questions about the notification system, check:
1. Browser console for SignalR connection errors
2. Network tab for API call failures
3. Database for notification records
4. SignalR hub connection status

**Happy Coding! ??**
