using Linkify.Domain.Shared;
using System.Linq.Expressions;

namespace Linkify.Domain.Specifications
{
    public abstract class BaseSpecification<TEntity>
    {

        public Expression<Func<TEntity, bool>>? Criteria { get; protected set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; } = [];
        //public List<OrderExpression<TEntity>> OrderByExpressions { get; } = [];

        //public void ApplyOrderBy(OrderExpression<TEntity> orderExpression)
        //{
        //    OrderByExpressions.Add(orderExpression);
        //}

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
