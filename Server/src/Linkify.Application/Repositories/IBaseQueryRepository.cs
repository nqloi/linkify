using Linkify.Domain.Bases;
using Linkify.Domain.Specifications;
using System.Linq.Expressions;

namespace Linkify.Application.Repositories
{
    public interface IBaseQueryRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Query();
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetWithSpecificationAsync(BaseSpecification<TEntity> specification);
    }
}
