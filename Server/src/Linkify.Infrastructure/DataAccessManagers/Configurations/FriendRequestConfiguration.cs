using Linkify.Domain.Aggregates.FriendshipAggregate;
using Linkify.Infrastructure.DataAccessManagers.Configurations.Bases;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linkify.Infrastructure.DataAccessManagers.Configurations;

public class FriendRequestConfiguration : BaseEntityConfiguration<FriendRequest>
{
    public override void Configure(EntityTypeBuilder<FriendRequest> builder)
    {
        base.Configure(builder);

        builder.Property(fr => fr.SenderId)
            .IsRequired();

        builder.Property(fr => fr.ReceiverId)
            .IsRequired();

        builder.Property(fr => fr.Status)
            .IsRequired();

        builder.Property(fr => fr.Message)
            .HasMaxLength(500)
            .IsRequired(false);

        // Index để tìm kiếm friend requests theo sender/receiver
        builder.HasIndex(fr => fr.SenderId);
        builder.HasIndex(fr => fr.ReceiverId);

        // Index kết hợp để tìm nhanh pending requests
        builder.HasIndex(fr => new { fr.ReceiverId, fr.Status });
    }
}