using AutoMapper;
using Linkify.Application.Features.Comments.Common;
using Linkify.Application.Features.Common;
using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Aggregates.UserProfileAggregate;

namespace Linkify.Application.Mapping
{
    public static class CommentMappingConfiguration
    {
        public static void Configure(Profile profile)
        {
            profile.CreateMap<Comment, CommentDto>()
                .ForMember(des => des.Creator, opt => opt.MapFrom(src => src.UserProfile));
        }
    }
}
