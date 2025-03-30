using Linkify.Application.Common.Helper;
using Linkify.Application.Common.Models;
using Linkify.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Linkify.Infrastructure.Extensions
{
    public static class PaginationExtensions
    {
        public static async Task<CursorPaginatedResult<TEntity>> ApplyCursorPagination<TEntity>(
            this IQueryable<TEntity> query,
            CursorPaginationParameters parameters,
            params SortCriteria[] sortCriteriaList)
        {
            if (sortCriteriaList.Length == 0)
                throw new ArgumentException("At least one sort criteria is required");

            var cursorValues = CursorPagedHelper.DecodeCursor(parameters.Cursor);

            // Apply cursor filter
            if (cursorValues.Count > 0)
            {
                query = ApplyCursorFilter(query, cursorValues, sortCriteriaList);
            }

            // Apply ordering
            query = ApplyOrdering(query, sortCriteriaList);

            var items = await query.Take(parameters.Limit + 1).ToListAsync();
            var hasNextPage = items.Count > parameters.Limit;

            return new CursorPaginatedResult<TEntity>
            {
                Items = hasNextPage ? items.Take(parameters.Limit).ToList() : items,
                Cursor = hasNextPage ? CursorPagedHelper.CreateCursor(items[parameters.Limit - 1], sortCriteriaList) : null,
                HasNextPage = hasNextPage
            };
        }

        private static IQueryable<TEntity> ApplyCursorFilter<TEntity>(
            IQueryable<TEntity> query,
            Dictionary<string, object> cursorValues,
            SortCriteria[] sortCriteriaList)
        {
            ParameterExpression param = Expression.Parameter(typeof(TEntity), "e");
            Expression? cursorCondition = null;

            foreach (var criteria in sortCriteriaList)
            {
                if (!cursorValues.TryGetValue(criteria.FieldName, out var cursorValue))
                    continue;

                var property = typeof(TEntity).GetProperty(criteria.FieldName, BindingFlags.Public | BindingFlags.Instance);
                if (property == null) continue;

                var left = Expression.Property(param, property);
                var right = Expression.Constant(Convert.ChangeType(cursorValue, property.PropertyType), property.PropertyType);

                var comparison = criteria.IsDescending
                    ? Expression.LessThan(left, right)
                    : Expression.GreaterThan(left, right);

                cursorCondition = cursorCondition == null
                    ? comparison
                    : Expression.OrElse(cursorCondition, comparison);
            }

            if (cursorCondition != null)
            {
                var lambda = Expression.Lambda<Func<TEntity, bool>>(cursorCondition, param);
                query = query.Where(lambda);
            }

            return query;
        }

        private static IQueryable<TEntity> ApplyOrdering<TEntity>(
            IQueryable<TEntity> query,
            SortCriteria[] sortCriteriaList)
        {
            if (sortCriteriaList.Length == 0)
                return query;

            IOrderedQueryable<TEntity>? orderedQuery = null;

            foreach (var criteria in sortCriteriaList)
            {
                var property = typeof(TEntity).GetProperty(criteria.FieldName, BindingFlags.Public | BindingFlags.Instance);
                if (property == null) continue;

                if (orderedQuery == null)
                {
                    orderedQuery = criteria.IsDescending
                        ? query.OrderByDescending(e => EF.Property<object>(e, criteria.FieldName))
                        : query.OrderBy(e => EF.Property<object>(e, criteria.FieldName));
                }
                else
                {
                    orderedQuery = criteria.IsDescending
                        ? orderedQuery.ThenByDescending(e => EF.Property<object>(e, criteria.FieldName))
                        : orderedQuery.ThenBy(e => EF.Property<object>(e, criteria.FieldName));
                }
            }

            return orderedQuery ?? query;
        }
    }
}
