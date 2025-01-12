using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Aggregates.Token;
using Linkify.Domain.Aggregates.UserProfile;
using Linkify.Domain.Constants;
using Linkify.Infrastructure.DataAccessManagers.Configurations.Bases;
using Linkify.Infrastructure.SecurityManagers.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Infrastructure.DataAccessManagers.Configurations
{
    public class UserProfileConfiguration : BaseEntityConfiguration<UserProfile>
    {
        public override void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            base.Configure(builder);

            builder.HasKey(up => up.Id);

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(up => up.AvatarUrl)
                .HasMaxLength(LengthConst.L);

            builder.Property(up => up.DisplayName)
                .IsRequired()
                .HasMaxLength(LengthConst.M);

            builder.HasIndex(up => up.UserId)
                .IsUnique();

            builder.Property(up => up.Bio)
                .HasMaxLength(LengthConst.L);

            builder.HasOne<ApplicationUser>()
                .WithMany() 
                .HasForeignKey(up => up.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
