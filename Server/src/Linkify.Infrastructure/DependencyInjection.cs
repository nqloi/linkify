using Linkify.Infrastructure.DataAccessManagers;
using Linkify.Infrastructure.FileManagers;
using Linkify.Infrastructure.RealtimeManagers;
using Linkify.Infrastructure.RealtimeManagers.NotificationManagers;
using Linkify.Infrastructure.SecurityManagers.Identity;
using Linkify.Infrastructure.SecurityManagers.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Linkify.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDataAccess(configuration);

            services.RegisterToken(configuration);

            services.RegisterIdentity(configuration);

            services.RegisterFileManager(configuration);

            services.RegisterRealtimeManagers();

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
