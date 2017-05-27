using System.Linq.Expressions;

namespace Orion.Scripting.Ast
{
    internal class AstGreaterThanOrEqualOperator : AstOperator
    {
        public AstGreaterThanOrEqualOperator(string value) : base(value) { }

        protected override Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right)
        {
            return Expression.GreaterThanOrEqual(left, right);
        }
    }
}