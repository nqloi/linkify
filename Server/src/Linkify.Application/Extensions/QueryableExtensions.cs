using Linkify.Application.Common.Helper;
using Linkify.Application.Common.Models;
using Linkify.Domain.Interfaces;
using Linkify.Domain.Shared;
using System.Linq.Expressions;

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
