using System.Linq.Expressions;

namespace Orion.Scripting.Ast
{
    internal class AstSubtractOperator : AstOperator
    {
        public AstSubtractOperator(string value) : base(value) { }

        protected override Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right)
        {
            return Expression.Subtract(left, right);
        }
    }
}