using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Constants;
using Linkify.Infrastructure.DataAccessManagers.Configurations.Bases;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Linkify.Infrastructure.DataAccessManagers.Configurations
{
    public class ReactionConfiguration : BaseEntityConfiguration<Reaction>
    {
        public override void Configure(EntityTypeBuilder<Reaction> builder)
        {
            base.Configure(builder);

            // Properties
            builder.Property(c => c.UserId)
                .IsRequired();

            builder.Property(c => c.PostId)
                .IsRequired();

            builder.Property(c => c.Type)
                .IsRequired();

            // Post (Many-to-One)
            builder.HasOne(c => c.Post)
                .WithMany(p => p.Reactions)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete comments when the post is deleted
        }
    }
}
