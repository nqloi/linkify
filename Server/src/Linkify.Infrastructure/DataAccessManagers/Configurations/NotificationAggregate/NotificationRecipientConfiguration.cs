using Linkify.Domain.Aggregates.NotificationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linkify.Infrastructure.DataAccessManagers.Configurations.NotificationAggregate
{
    public class NotificationRecipientConfiguration : IEntityTypeConfiguration<NotificationRecipient>
    {
        public void Configure(EntityTypeBuilder<NotificationRecipient> builder)
        {
            builder.HasOne(nr => nr.Recipient)
                   .WithMany()
                   .HasForeignKey(nr => nr.RecipientId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(nr => nr.Notification)
                   .WithMany(n => n.Recipients)
                   .HasForeignKey(nr => nr.NotificationId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(nr => nr.IsRead)
                   .IsRequired();
        }
    }
}
