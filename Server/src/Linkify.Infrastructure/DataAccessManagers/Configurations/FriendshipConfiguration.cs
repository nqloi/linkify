using Linkify.Domain.Aggregates.FriendshipAggregate;
using Linkify.Infrastructure.DataAccessManagers.Configurations.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linkify.Infrastructure.DataAccessManagers.Configurations;

public class FriendshipConfiguration : BaseEntityConfiguration<Friendship>
{
    public override void Configure(EntityTypeBuilder<Friendship> builder)
    {
        base.Configure(builder);

        builder.Property(f => f.PrimaryUserId)
            .IsRequired();

        builder.Property(f => f.SecondaryUserId)
            .IsRequired();

        builder.Property(f => f.Status)
            .IsRequired();

        builder.Property(f => f.FriendsSince)
            .IsRequired(false);

        // Unique constraint to prevent duplicate friendships
        builder.HasIndex(f => new { f.PrimaryUserId, f.SecondaryUserId })
            .IsUnique();

        // Relationships
        builder.HasMany(f => f.FriendRequests)
            .WithOne()
            .HasForeignKey(fr => fr.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}