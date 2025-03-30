using AutoMapper;
using Linkify.Application.Features.Posts.Queries.GetPost;
using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Shared;
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
            UserProfileMappingConfiguration.Configure(this);
            PostMappingConfiguration.Configure(this);
            CommentMappingConfiguration.Configure(this);
        }
    }
}

