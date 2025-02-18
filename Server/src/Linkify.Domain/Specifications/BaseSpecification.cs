using Linkify.Domain.Bases;
using System.Linq.Expressions;

namespace Linkify.Domain.Specifications
{
    public abstract class BaseSpecification<TEntity> where TEntity : BaseEntity
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; protected set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; } = [];

        public List<(Expression<Func<TEntity, object>> KeySelector, bool IsDescending)> OrderByExpressions { get; } = [];

        public int? Skip { get; private set; }
        public int? Take { get; private set; }

        public void ApplyOrderBy(Expression<Func<TEntity, object>> keySelector)
        {
            OrderByExpressions.Add((keySelector, false)); // ASC
        }

        public void ApplyOrderByDescending(Expression<Func<TEntity, object>> keySelector)
        {
            OrderByExpressions.Add((keySelector, true)); // DESC
        }

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
