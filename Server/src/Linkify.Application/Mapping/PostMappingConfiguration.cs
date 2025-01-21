using AutoMapper;
using Linkify.Application.Features.Posts.Common;
using Linkify.Application.Features.Posts.Queries.GetPost;
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
                .ForMember(des => des.Creator, opt => opt.MapFrom(src => src.UserProfile));

            profile.CreateMap<UserProfile, CreatorDto>();
        }
    }
}
