using ErrorOr;
using Linkify.Application.Features.Comments.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Comments.Commands.CreateComment
{
    public record CreateCommentCommand : IRequest<ErrorOr<Guid>>
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public required string Content { get; set; }
    }
}
