using AutoMapper;
using ErrorOr;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.UserProfileAggregate;
using Linkify.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.UserProfiles.Queries.Get
{
    public class GetUserProfileQueryHandler : BaseQueryHandler<UserProfile>, IRequestHandler<GetUserProfileQuery, ErrorOr<UserProfileDto>>
    {
        public GetUserProfileQueryHandler(IBaseQueryRepository<UserProfile> repository, ICurrentUserService currentUserService, IMapper mapper) : base(repository, currentUserService, mapper)
        {
        }

        public async Task<ErrorOr<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var userProfile = await _repository.FirstOrDefaultAsync(u => u.UserId == request.UserId);

            if (userProfile == null)
            {
                return Error.NotFound("GetUserProfileQuery.Get", "User not found");
            }

            return _mapper.Map<UserProfileDto>(userProfile);
        }
    }
}
