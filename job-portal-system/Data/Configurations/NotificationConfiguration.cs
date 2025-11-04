using job_portal_system.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace job_portal_system.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
   {
      builder.HasKey(n => n.Id);

     builder.Property(n => n.UserId)
                .IsRequired();

         builder.Property(n => n.Title)
 .IsRequired()
         .HasMaxLength(200);

     builder.Property(n => n.Message)
 .IsRequired()
  .HasMaxLength(500);

     builder.Property(n => n.Type)
         .IsRequired()
  .HasConversion<string>();

  builder.Property(n => n.IsRead)
      .IsRequired()
        .HasDefaultValue(false);

            builder.Property(n => n.CreatedAt)
     .IsRequired();

        builder.Property(n => n.RelatedEntityId)
    .HasMaxLength(50);

  builder.Property(n => n.ActionUrl)
   .HasMaxLength(500);

  builder.HasOne(n => n.User)
     .WithMany(u => u.Notifications)
        .HasForeignKey(n => n.UserId)
      .OnDelete(DeleteBehavior.Cascade);

  builder.HasIndex(n => new { n.UserId, n.IsRead });
        }
 }
}
