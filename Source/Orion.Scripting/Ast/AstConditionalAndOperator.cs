using System.Linq.Expressions;

namespace Orion.Scripting.Ast
{
    internal class AstConditionalAndOperator : AstOperator
    {
        public AstConditionalAndOperator(string value) : base(value) { }

        protected override Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right)
        {
            return Expression.And(left, right);
        }
    }
}