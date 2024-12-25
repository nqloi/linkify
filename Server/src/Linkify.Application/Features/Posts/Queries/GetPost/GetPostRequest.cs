using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Posts.Queries.GetPost
{
    public record GetPostRequest : IRequest<IEnumerable<GetPostDto>>
    {
    }
}
