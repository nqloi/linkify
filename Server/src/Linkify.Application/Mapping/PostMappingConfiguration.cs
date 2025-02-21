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
            profile.CreateMap<Post, GetPostDto>()
                .ForMember(des => des.ImageUrls, opt => opt.MapFrom(src => src.PostImages.Select(pi => pi.ImageUrl).ToList()))
                .ForMember(des => des.Creator, opt => opt.MapFrom(src => new CreatorDto
                {
                    Id = src.UserId,
                    DisplayName = src.UserProfile.DisplayName,

                }))
                .ForMember(des => des.Stats, opt => opt.MapFrom(src => new PostStatsDto
                {
                    CommentCount = src.Comments.Count,
                    ReactionCount = src.Reactions.Count,
                }));
        }
    }
}
