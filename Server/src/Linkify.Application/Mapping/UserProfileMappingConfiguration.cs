using AutoMapper;
using Linkify.Application.Features.Common;
using Linkify.Application.Features.Posts.Common;
using Linkify.Application.Features.UserProfiles.Queries.Get;
using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Aggregates.UserProfileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Mapping
{
    public static class UserProfileMappingConfiguration
    {
        public static void Configure(Profile profile)
        {
            profile.CreateMap<UserProfile, CreatorDto>();

            profile.CreateMap<UserProfile, UserProfileDto>();
        }
    }
}
