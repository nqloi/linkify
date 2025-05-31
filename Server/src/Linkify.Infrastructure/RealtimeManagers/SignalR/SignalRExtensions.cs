using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Linkify.Infrastructure.RealtimeManagers.SignalR
{
    public static class SignalRExtensions
    {
        public static IServiceCollection AddSignalRServices(this IServiceCollection services)
        {
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.MaximumReceiveMessageSize = 102400; // 100 KB
            });

            services.AddCors(options =>
            {
                options.AddPolicy("SignalRPolicy", builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:3000"); // Add your client URL here
                });
            });

            return services;
        }

        public static WebApplication UseSignalRServices(this WebApplication app)
        {
            app.UseCors("SignalRPolicy");
            app.MapHub<NotificationHub>("/hubs/notification");
            
            return app;
        }
    }
}
