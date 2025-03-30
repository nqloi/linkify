using Linkify.Application.Common.Models;
using Linkify.Domain.Bases;
using Linkify.Domain.Specifications;
using System.Linq.Expressions;

namespace Linkify.Application.Repositories
{
    public interface IBaseQueryRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Get an entity by its ID.
        /// </summary>
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all entities.
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Find entities matching the given condition.
        /// </summary>
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a queryable collection of entities without tracking.
        /// </summary>
        IQueryable<TEntity> AsNoTrackingQuery();

        /// <summary>
        /// Check if any entity matches the given condition.
        /// </summary>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Count the number of entities matching the given condition.
        /// </summary>
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get entities based on a specification.
        /// </summary>
        Task<IEnumerable<TEntity>> GetWithSpecificationAsync(BaseSpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a queryable collection of entities based on a specification.
        /// </summary>
        IQueryable<TEntity> GetWithSpecification(BaseSpecification<TEntity> spec);

        /// <summary>
        /// Executes the given query asynchronously and returns the result as a list.
        /// </summary>
        Task<List<T>> QueryAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes the given query asynchronously and returns the result as a TEntity or default.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        //Task<List<T>> GetCursorPagingList<T>(IQueryable<T> query, 
        //    CursorPaginationParameters cursorPaginationParameters, 
        //    CancellationToken cancellationToken = default);
    }
}
