using Linkify.Application.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Linkify.Infrastructure.DataAccessManagers.Context
{
    public class CommandContext(DbContextOptions<ApplicationDbContext> options) : ApplicationDbContext(options), ICommandContext
    {
    }
}
