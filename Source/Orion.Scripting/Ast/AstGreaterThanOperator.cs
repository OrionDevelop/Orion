using System.Linq.Expressions;

namespace Orion.Scripting.Ast
{
    internal class AstGreaterThanOperator : AstOperator
    {
        public AstGreaterThanOperator(string value) : base(value) { }

        protected override Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right)
        {
            return Expression.GreaterThan(left, right);
        }
    }
}