using Linkify.Application.ExternalServices;
using Linkify.Infrastructure.RealtimeManagers.ChatManagers;
using Linkify.Infrastructure.RealtimeManagers.NotificationManagers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Infrastructure.RealtimeManagers
{
    public static class DI
    {
        public static IServiceCollection RegisterRealtimeManagers(this IServiceCollection services) {
            services.AddSignalR();
            services.AddScoped<IChatService, SignalRChatService>();
            services.AddScoped<INotificationService, SignalRNotificationService>();

            return services;
        }
    }
}
