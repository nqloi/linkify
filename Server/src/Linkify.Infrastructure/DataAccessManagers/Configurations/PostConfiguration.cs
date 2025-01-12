using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Aggregates.Token;
using Linkify.Domain.Constants;
using Linkify.Infrastructure.DataAccessManagers.Configurations.Bases;
using Linkify.Infrastructure.SecurityManagers.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Infrastructure.DataAccessManagers.Configurations
{
    public class PostConfiguration : BaseEntityConfiguration<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            base.Configure(builder);

            // Properties
            builder.Property(p => p.UserId)
                .IsRequired();

            // PostImages (One-to-Many)
            builder.HasMany(p => p.PostImages)
                 .WithOne(pi => pi.Post)
                 .HasForeignKey(pi => pi.PostId)
                 .OnDelete(DeleteBehavior.Cascade);

            // Comments (One-to-Many)
            builder.HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Reactions (One-to-Many)
            builder.HasMany(p => p.Reactions)
                .WithOne(r => r.Post)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
