using Linkify.Domain.Aggregates.UserProfileAggregate;
using Linkify.Domain.Constants;
using Linkify.Infrastructure.DataAccessManagers.Configurations.Bases;
using Linkify.Infrastructure.SecurityManagers.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.Property(up => up.UserName)
                .IsRequired()
                .HasMaxLength(LengthConst.M);

            builder.Property(up => up.AvatarUrl)
                .HasMaxLength(LengthConst.L);

            builder.Property(up => up.DisplayName)
                .IsRequired()
                .HasMaxLength(LengthConst.M);

            builder.HasIndex(up => up.UserId)
                .IsUnique();

            builder.HasIndex(up => up.UserName)
                .IsUnique();

            builder.Property(up => up.Bio)
                .HasMaxLength(LengthConst.L);

            builder.Property(up => up.Email)
                .HasMaxLength(LengthConst.M);

            builder.Property(up => up.PhoneNumber)
                .HasMaxLength(LengthConst.M);

            builder.Property(up => up.DateOfBirth)
                .HasColumnType("date");

            builder.Property(up => up.Address)
                .HasMaxLength(LengthConst.L);

            builder.Property(up => up.Location)
                .HasMaxLength(LengthConst.M);

            builder.Property(up => up.Work)
                .HasMaxLength(LengthConst.M);

            builder.Property(up => up.Education)
                .HasMaxLength(LengthConst.M);

            builder.Property(up => up.Relationship)
                .HasMaxLength(LengthConst.M);

            builder.Property(up => up.JoinedDate)
                .IsRequired();

            builder.HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<UserProfile>(up => up.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
