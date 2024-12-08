using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Authentication.Queries.CheckDuplicateUsername
{
    public class CheckDuplicateUsernameRequest : IRequest<bool>
    {
        public required string Username { get; init; }
    }
}
