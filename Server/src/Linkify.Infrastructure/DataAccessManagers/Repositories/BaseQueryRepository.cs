using Linkify.Application.Repositories;
using Linkify.Domain.Bases;
using Linkify.Domain.Specifications;
using Linkify.Infrastructure.DataAccessManagers.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Linkify.Infrastructure.DataAccessManagers.Repositories
{
    public class BaseQueryRepository<TEntity>(ApplicationDbContext context) : IBaseQueryRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context = context;
        protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().CountAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetWithSpecificationAsync(BaseSpecification<TEntity> specification)
        {
            var query = SpecificationEvaluator<TEntity>.GetQuery(_dbSet.AsQueryable(), specification);
            return await query.ToListAsync();
        }
    }
}
