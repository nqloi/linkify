using Linkify.Application.Extensions;
using Linkify.Application.Repositories;
using Linkify.Domain.Bases;
using Linkify.Domain.Interfaces;
using Linkify.Infrastructure.DataAccessManagers.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Linkify.Infrastructure.DataAccessManagers.Repositories
{
    public class BaseCommandRepository<T> : IBaseCommandRepository<T> where T : BaseEntity, IAggregateRoot
    {
        protected readonly ApplicationDbContext _context;

        public BaseCommandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Add(entity);
        }

        public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(entity, cancellationToken);
        }

        public void Delete(T entity)
        {
            _context.Update(entity);
        }

        public void DeleteById(Guid id)
        {
            var entity = _context.Find(typeof(T), id);
            if (entity != null)
            {
                _context.Remove(entity);
            }
        }

        public T? Get(Guid id)
        {
            var entity = _context.Set<T>().ApplyIsDeletedFilter().SingleOrDefault(x => x.Id == id);
            return entity;
        }

        public async Task<T?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Set<T>()
             .ApplyIsDeletedFilter()
             .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return entity;
        }

        public IQueryable<T> GetQuery()
        {
            var query = _context.Set<T>().AsQueryable();

            return query;
        }

        public void Purge(T entity)
        {
            _context.Remove(entity);
        }

        public Task PurgeAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void UpdatePartial(T entity, params Expression<Func<T, object>>[] properties)
        {
            _context.Update(entity);
            foreach (var property in properties)
            {
                _context.Entry(entity).Property(property).IsModified = true;
            }
        }

    }
}
