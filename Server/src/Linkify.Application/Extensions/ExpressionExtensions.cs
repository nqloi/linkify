using System.Linq.Expressions;

namespace Linkify.Application.Extensions
{
    public static class ExpressionExtensions
    {
        public static string GetPropertyName<TEntity, TProperty>(this Expression<Func<TEntity, TProperty>> expression)
        {
            return expression.Body switch
            {
                MemberExpression m => m.Member.Name,
                UnaryExpression u when u.Operand is MemberExpression m => m.Member.Name,
                _ => throw new ArgumentException("Invalid property expression")
            };
        }

        public static string GetPropertyName<TEntity>(this Expression<Func<TEntity, object>> expression)
        {
            return GetPropertyName<TEntity, object>(expression);
        }
    }
}
