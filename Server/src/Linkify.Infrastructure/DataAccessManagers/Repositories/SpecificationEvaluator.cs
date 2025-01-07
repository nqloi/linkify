using Linkify.Domain.Bases;
using Linkify.Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Infrastructure.DataAccessManagers.Repositories
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, BaseSpecification<TEntity> spec)
        {
            IQueryable<TEntity> query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            foreach (var include in spec.Includes)
            {
                query = query.Include(include);
            }

            bool isFirstOrderBy = true;
            foreach (var (keySelector, isDescending) in spec.OrderByExpressions)
            {
                if (isFirstOrderBy)
                {
                    query = isDescending
                        ? query.OrderByDescending(keySelector)
                        : query.OrderBy(keySelector);
                    isFirstOrderBy = false;
                }
                else
                {
                    query = isDescending
                        ? ((IOrderedQueryable<TEntity>)query).ThenByDescending(keySelector)
                        : ((IOrderedQueryable<TEntity>)query).ThenBy(keySelector);
                }
            }

            if (spec.Skip.HasValue && spec.Take.HasValue)
            {
                query = query.Skip(spec.Skip.Value).Take(spec.Take.Value);
            }

            return query;
        }
    }
}
