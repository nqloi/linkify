using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Bases;
using Linkify.Domain.Specifications;
using Linkify.Infrastructure.DataAccessManagers.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;

namespace Linkify.Infrastructure.DataAccessManagers.Repositories
{
    public class BaseQueryRepository<TEntity>(ApplicationDbContext context) : IBaseQueryRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context = context;
        protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
        }

        public IQueryable<TEntity> AsNoTrackingQuery()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().AnyAsync(predicate, cancellationToken);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.CountAsync();
        }


        public async Task<IEnumerable<TEntity>> GetWithSpecificationAsync(BaseSpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            var query = SpecificationEvaluator<TEntity>.GetQuery(_dbSet.AsQueryable(), specification);
            return await query.ToListAsync(cancellationToken);
        }

        public IQueryable<TEntity> GetWithSpecification(BaseSpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_dbSet.AsQueryable(), spec);
        }

        public async Task<List<T>> QueryAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            return await query.ToListAsync(cancellationToken);
        }

    }
}
