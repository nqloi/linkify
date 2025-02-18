using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Aggregates.UserProfileAggregate;
using Linkify.Infrastructure.DataAccessManagers.Configurations.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linkify.Infrastructure.DataAccessManagers.Configurations
{
    public class CommentConfiguration : BaseEntityConfiguration<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            base.Configure(builder);

            // Properties
            builder.Property(c => c.UserId)
                .IsRequired();

            builder.Property(c => c.PostId)
                .IsRequired();

            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(1000); // Set a maximum length for the comment content

            // UserProfile (Many-to-One)
            builder.HasOne(c => c.UserProfile)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .HasPrincipalKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Post (Many-to-One)
            builder.HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete comments when the post is deleted
        }
    }
}