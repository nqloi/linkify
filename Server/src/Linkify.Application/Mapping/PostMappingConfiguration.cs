using AutoMapper;
using Linkify.Application.Features.Common;
using Linkify.Application.Features.Posts.Common;
using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Aggregates.UserProfileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Mapping
{
    public static class PostMappingConfiguration
    {
        public static void Configure(Profile profile)
        {
            profile.CreateMap<Reaction, UserPostActionsDto>()
            .ForMember(des => des.ReactionType, opt => opt.MapFrom(src => src.Type));
        }
    }
}
