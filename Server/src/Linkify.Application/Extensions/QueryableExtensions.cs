using Linkify.Domain.Interfaces;

namespace Linkify.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyIsDeletedFilter<T>(this IQueryable<T> query, bool isDeleted = false) where T : class
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                query = query.Where(x => (x as ISoftDelete)!.IsDeleted == isDeleted);
            }
            return query;
        }
    }
}
