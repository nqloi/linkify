using Linkify.Domain.Aggregates.FriendshipAggregate;
using Linkify.Domain.Aggregates.NotificationAggregate;
using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Aggregates.Token;
using Linkify.Domain.Aggregates.UserProfileAggregate;
using Linkify.Infrastructure.DataAccessManagers.Configurations;
using Linkify.Infrastructure.SecurityManagers.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Linkify.Infrastructure.DataAccessManagers.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid> 
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Token> Token { get; set; }

        public DbSet<Post> Post { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<Reaction> Reaction { get; set; }

        public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<FriendRequest> FriendRequest { get; set; }

        public DbSet<Friendship> Friendship { get; set; }

        public DbSet<Notification> Notification { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TokenConfiguration());

            modelBuilder.ApplyConfiguration(new UserProfileConfiguration());

            modelBuilder.ApplyConfiguration(new PostConfiguration());

            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            modelBuilder.ApplyConfiguration(new ReactionConfiguration());

            modelBuilder.ApplyConfiguration(new FriendRequestConfiguration());

            modelBuilder.ApplyConfiguration(new FriendshipConfiguration());
        }
    }
}
