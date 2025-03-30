using Linkify.Application.Common.Helper;
using Linkify.Application.Common.Models;
using Linkify.Domain.Specifications;
using Linkify.Domain.Specifications.Paging;
using Microsoft.EntityFrameworkCore;

namespace Linkify.Infrastructure.DataAccessManagers.Repositories
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> inputQuery, BaseSpecification<TEntity> spec) where TEntity : class
        {
            IQueryable<TEntity> query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            //bool isFirstOrderBy = true;
            //foreach (var (keySelector, isDescending) in spec.OrderByExpressions)
            //{
            //    if (isFirstOrderBy)
            //    {
            //        query = isDescending
            //            ? query.OrderByDescending(keySelector)
            //            : query.OrderBy(keySelector);
            //        isFirstOrderBy = false;
            //    }
            //    else
            //    {
            //        query = isDescending
            //            ? ((IOrderedQueryable<TEntity>)query).ThenByDescending(keySelector)
            //            : ((IOrderedQueryable<TEntity>)query).ThenBy(keySelector);
            //    }
            //}

            return query;
        }
    }
}
