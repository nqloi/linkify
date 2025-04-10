using AutoMapper;
using Linkify.Application.Features.Notifications.Common;
using Linkify.Domain.Aggregates.NotificationAggregate;

namespace Linkify.Application.Mapping
{
    public static class NotificationMappingConfiguration
    {
        public static void Configure(Profile profile)
        {
            profile.CreateMap<Notification, GetNotificationDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.IsRead, opt => opt.MapFrom(src => src.IsRead))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.ActionUrl, opt => opt.MapFrom(src => src.ActionUrl));
        }
    }
}
