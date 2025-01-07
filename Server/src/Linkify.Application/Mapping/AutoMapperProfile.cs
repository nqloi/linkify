using AutoMapper;
using Linkify.Application.Features.Posts.Queries.GetPost;
using Linkify.Domain.Aggregates.PostAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, GetPostDto>()
            .ForMember(des => des.ImageUrls, opt => opt.MapFrom(src => src.PostImages.Select(pi => pi.ImageUrl).ToList()));
        }
    }
}

