using Linkify.Application.Repositories;
using Linkify.Infrastructure.DataAccessManagers.Context;

namespace Linkify.Infrastructure.DataAccessManagers.Repositories
{
    public class UnitOfWork(ApplicationDbContext commandContext) : IUnitOfWork
    {
        public void SaveChanges()
        {
            commandContext.SaveChanges();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await commandContext.SaveChangesAsync(cancellationToken); 
        }
    }
}
