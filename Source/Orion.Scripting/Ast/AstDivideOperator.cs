using System.Linq.Expressions;

namespace Orion.Scripting.Ast
{
    internal class AstDivideOperator : AstOperator
    {
        public AstDivideOperator(string value) : base(value) { }

        protected override Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right)
        {
            return Expression.Divide(left, right);
        }
    }
}