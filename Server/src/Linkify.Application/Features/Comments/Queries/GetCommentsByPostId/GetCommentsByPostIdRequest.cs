﻿using ErrorOr;
using Linkify.Application.Features.Comments.Common;
using MediatR;

namespace Linkify.Application.Features.Comments.Queries.GetCommentsByPostId
{
    public class GetCommentsByPostIdRequest : IRequest<ErrorOr<List<CommentDto>>>
    {
        public required Guid PostId { get; set; }
    }
}
