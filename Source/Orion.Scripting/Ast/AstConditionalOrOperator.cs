using System.Linq.Expressions;

namespace Orion.Scripting.Ast
{
    internal class AstConditionalOrOperator : AstOperator
    {
        public AstConditionalOrOperator(string value) : base(value) { }

        protected override Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right)
        {
            return Expression.Or(left, right);
        }
    }
}