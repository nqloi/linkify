using FluentValidation;
using Linkify.Application.Common.Behaviors;
using Linkify.Application.Mapping;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Linkify.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            return services;
        }
    }
}
