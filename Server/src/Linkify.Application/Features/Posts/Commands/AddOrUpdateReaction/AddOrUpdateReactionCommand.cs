using ErrorOr;
using Linkify.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Posts.Commands.AddOrUpdateReaction
{
    public class AddOrUpdateReactionCommand : IRequest<ErrorOr<bool>>
    {
        public ReactionType Type { get; set; }
        public Guid PostId { get; set; }
    }
}
