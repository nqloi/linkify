using AutoMapper;

namespace Linkify.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            UserProfileMappingConfiguration.Configure(this);
            PostMappingConfiguration.Configure(this);
            CommentMappingConfiguration.Configure(this);
            NotificationMappingConfiguration.Configure(this);
        }
    }
}
