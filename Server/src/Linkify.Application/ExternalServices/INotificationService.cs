using Linkify.Domain.Aggregates.NotificationAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.ExternalServices
{
    public interface INotificationService
    {
        Task SendNotificationAsync(Guid userId, string notification);
        Task SendNotificationAsync(Notification notification);
        Task SendToGroupAsync(string groupName, string notification);
        Task SendToMultipleUsersAsync(IEnumerable<Guid> userIds, string notification);
    }
}
