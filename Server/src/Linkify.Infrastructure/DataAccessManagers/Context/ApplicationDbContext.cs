using Linkify.Domain.Aggregates.Token;
using Linkify.Infrastructure.DataAccessManagers.Configurations;
using Linkify.Infrastructure.SecurityManagers.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Linkify.Infrastructure.DataAccessManagers.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> 
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Token> Token { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TokenConfiguration());
        }
    }
}
