using Linkify.Domain.Aggregates.NotificationAggregate;
using Linkify.Domain.Constants;
using Linkify.Infrastructure.DataAccessManagers.Configurations.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linkify.Infrastructure.DataAccessManagers.Configurations
{
    public class NotificationConfiguration : BaseEntityConfiguration<Notification>
    {
        public override void Configure(EntityTypeBuilder<Notification> builder)
        {
            base.Configure(builder);

            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(LengthConst.M);

            builder.Property(n => n.Message)
                .IsRequired()
                .HasMaxLength(LengthConst.L);

            builder.Property(n => n.Type)
                .IsRequired();

            builder.Property(n => n.CreatedAt)
                .IsRequired(false);

            // Optional fields
            builder.Property(n => n.ActionUrl)
                .HasMaxLength(LengthConst.L)
                .IsRequired(false);

            // Indexes for efficient querying
            builder.HasIndex(n => n.Type);
            builder.HasIndex(n => n.CreatedAt);
        }
    }
}
