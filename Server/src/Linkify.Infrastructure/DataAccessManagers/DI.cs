using Linkify.Application.Repositories;
using Linkify.Infrastructure.DataAccessManagers.Context;
using Linkify.Infrastructure.DataAccessManagers.Repositories;
using Linkify.Infrastructure.DataAccessManagers.Repositories.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Linkify.Infrastructure.DataAccessManagers;
public static class DI
{
    public static IServiceCollection RegisterDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 40)));
        });


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IBaseCommandRepository<>), typeof(BaseCommandRepository<>));
        services.AddScoped(typeof(IBaseQueryRepository<>), typeof(BaseQueryRepository<>));
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IPostRepository, PostRepository>();

        return services;
    }

    public static IHost CreateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        // Create database using ApplicationDbContext
        var dataContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        dataContext.Database.Migrate(); 

        return host;
    }
}
