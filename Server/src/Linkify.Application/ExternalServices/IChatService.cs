using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.ExternalServices
{
    public interface IChatService
    {
        Task SendMessageAsync(Guid receiverId, string message);
    }
}
