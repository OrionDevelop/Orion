using System.Linq.Expressions;

namespace Orion.Scripting.Ast
{
    internal class AstEqualOperator : AstOperator
    {
        public AstEqualOperator(string value) : base(value) { }

        protected override Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right)
        {
            return Expression.Equal(left, right);
        }
    }
}