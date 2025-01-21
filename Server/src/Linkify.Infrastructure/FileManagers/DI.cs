using Linkify.Application.Configurations;
using Linkify.Application.ExternalServices;
using Linkify.Infrastructure.FileManagers.CloudinaryService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Infrastructure.FileManagers
{
    public static class DI
    {
        public static IServiceCollection RegisterFileManager(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
            services.Configure<CloudFolderPathSettings>(configuration.GetSection("CloudFolderPaths"));

            services.AddTransient<IFileService, CloudinaryFileService>();

            return services;
        }
    }
}
