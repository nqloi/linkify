using Linkify.Infrastructure.SecurityManagers.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Linkify.Infrastructure.DataAccessManagers.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);


        //    builder.Entity<ApplicationUser>(entity =>
        //    {
        //        entity.ToTable("Users");  
        //    });

        //    builder.Entity<IdentityUserRole<string>>(entity =>
        //    {
        //        entity.ToTable("UserRoles"); 
        //    });

        //    builder.Entity<IdentityUserClaim<string>>(entity =>
        //    {
        //        entity.ToTable("UserClaims"); 
        //    });

        //    builder.Entity<IdentityUserLogin<string>>(entity =>
        //    {
        //        entity.ToTable("UserLogins"); 
        //    });

        //    builder.Entity<IdentityRoleClaim<string>>(entity =>
        //    {
        //        entity.ToTable("RoleClaims");  
        //    });

        //    builder.Entity<IdentityUserToken<string>>(entity =>
        //    {
        //        entity.ToTable("UserTokens"); 
        //    });
        //}
    }
}
