using Linkify.Infrastructure.DataAccessManagers.Context;
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
        //services.AddDbContext<CommandConte>(options =>
        //{
        //    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 40)));
        //});
        //services.AddDbContext<ApplicationDbContext>(options =>
        //{
        //    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 40)));
        //});b

        return services;
    }

    public static IHost CreateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        // Create database using ApplicationDbContext
        var dataContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        dataContext.Database.EnsureCreated(); // Ensure database is created (development only)

        return host;
    }
}
