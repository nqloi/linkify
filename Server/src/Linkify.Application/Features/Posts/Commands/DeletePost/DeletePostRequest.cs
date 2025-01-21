using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Posts.Commands.DeletePost
{
    public class DeletePostRequest : IRequest<bool>
    {
        public required Guid PostId { get; set; }
    }
}
