namespace job_portal_system.Models.Domain;

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

public class Notification
{
    public int Id { get; set; }
    public string UserId { get; set; } = "";
    public User User { get; set; } = default!;
    public string Title { get; set; } = "";
    public string Message { get; set; } = "";
    public NotificationType Type { get; set; }
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    // Optional: Link to related entity (JobId, ApplicationId, UserId, etc.)
    public string? RelatedEntityId { get; set; }
    public string? ActionUrl { get; set; } // URL to navigate when clicked
}