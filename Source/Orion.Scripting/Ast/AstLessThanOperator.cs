using System.Linq.Expressions;

namespace Orion.Scripting.Ast
{
    internal class AstLessThanOperator : AstOperator
    {
        public AstLessThanOperator(string value) : base(value) { }

        protected override Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right)
        {
            return Expression.LessThan(left, right);
        }
    }
}