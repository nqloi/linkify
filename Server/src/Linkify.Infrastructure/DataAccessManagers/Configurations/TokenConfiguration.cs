using Linkify.Domain.Aggregates.Token;
using Linkify.Domain.Constants;
using Linkify.Infrastructure.DataAccessManagers.Configurations.Bases;
using Linkify.Infrastructure.SecurityManagers.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Linkify.Infrastructure.DataAccessManagers.Configurations
{
    public class TokenConfiguration : BaseEntityConfiguration<Token>
    {
        public override void Configure(EntityTypeBuilder<Token> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.HasOne<ApplicationUser>()
            .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.RefreshToken)
                .HasMaxLength(LengthConst.M)
                .IsRequired();

            builder.HasIndex(e => e.UserId).HasDatabaseName("IX_Token_UserId");
            builder.HasIndex(e => e.RefreshToken).HasDatabaseName("IX_Token_RefreshToken");
        }
    }
}
