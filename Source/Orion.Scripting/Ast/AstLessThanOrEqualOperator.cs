using System.Linq.Expressions;

namespace Orion.Scripting.Ast
{
    internal class AstLessThanOrEqualOperator : AstOperator
    {
        public AstLessThanOrEqualOperator(string value) : base(value) { }

        protected override Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right)
        {
            return Expression.LessThanOrEqual(left, right);
        }
    }
}