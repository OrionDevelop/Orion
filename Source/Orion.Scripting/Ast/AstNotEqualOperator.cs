using System.Linq.Expressions;

namespace Orion.Scripting.Ast
{
    internal class AstNotEqualOperator : AstOperator
    {
        public AstNotEqualOperator(string value) : base(value) { }

        protected override Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right)
        {
            return Expression.NotEqual(left, right);
        }
    }
}