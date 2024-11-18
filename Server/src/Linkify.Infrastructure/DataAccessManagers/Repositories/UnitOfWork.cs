using Linkify.Application.Repositories;
using Linkify.Infrastructure.DataAccessManagers.Context;

namespace Linkify.Infrastructure.DataAccessManagers.Repositories
{
    public class UnitOfWork(CommandContext commandContext) : IUnitOfWork
    {
        public void Save()
        {
            commandContext.SaveChanges();
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await commandContext.SaveChangesAsync(cancellationToken);
        }
    }
}
