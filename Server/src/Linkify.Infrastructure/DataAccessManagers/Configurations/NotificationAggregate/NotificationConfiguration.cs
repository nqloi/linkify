using Linkify.Domain.Aggregates.NotificationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linkify.Infrastructure.DataAccessManagers.Configurations.NotificationAggregate
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasOne(n => n.Sender)
                   .WithMany()
                   .HasForeignKey(n => n.SenderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(n => n.Title)
                   .IsRequired();

            builder.Property(n => n.Message)
                   .IsRequired();

            builder.Property(n => n.Type)
                   .IsRequired();

            builder.HasMany(n => n.Recipients)
                   .WithOne(nr => nr.Notification)
                   .HasForeignKey(nr => nr.NotificationId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
