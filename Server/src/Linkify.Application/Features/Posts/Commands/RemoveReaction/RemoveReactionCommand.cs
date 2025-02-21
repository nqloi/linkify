using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Posts.Commands.RemoveReaction
{
    public class RemoveReactionCommand : IRequest<ErrorOr<bool>>
    {
        public Guid PostId { get; set; }
    }
}
