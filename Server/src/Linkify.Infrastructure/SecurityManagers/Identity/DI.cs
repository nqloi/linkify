using Linkify.Application.ExternalServices;
using Linkify.Infrastructure.DataAccessManagers.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Linkify.Infrastructure.SecurityManagers.Identity
{
    public static class DI
    {
        public static IServiceCollection RegisterIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var identitySectionName = "Identity";

            services.AddAuthorization();

            services.Configure<IdentitySettings>(configuration.GetSection(identitySectionName));

            services.AddIdentityApiEndpoints<ApplicationUser>(options =>
            {
                var identitySettings = configuration.GetSection(identitySectionName).Get<IdentitySettings>();
                if (identitySettings == null)
                {
                    return;
                }

                // Password settings
                options.Password.RequireDigit = identitySettings.Password.RequireDigit;
                options.Password.RequireLowercase = identitySettings.Password.RequireLowercase;
                options.Password.RequireUppercase = identitySettings.Password.RequireUppercase;
                options.Password.RequireNonAlphanumeric = identitySettings.Password.RequireNonAlphanumeric;
                options.Password.RequiredLength = identitySettings.Password.RequiredLength;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identitySettings.Lockout.DefaultLockoutTimeSpanInMinutes);
                options.Lockout.MaxFailedAccessAttempts = identitySettings.Lockout.MaxFailedAccessAttempts;
                options.Lockout.AllowedForNewUsers = identitySettings.Lockout.AllowedForNewUsers;

                // User settings
                options.User.RequireUniqueEmail = identitySettings.User.RequireUniqueEmail;

                // SignIn settings
                options.SignIn.RequireConfirmedEmail = identitySettings.SignIn.RequireConfirmedEmail;

            }).AddEntityFrameworkStores<ApplicationDbContext>();
                
            services.AddTransient<IIdentityService, IdentityService>(); 

            return services;
        }
    }
}
