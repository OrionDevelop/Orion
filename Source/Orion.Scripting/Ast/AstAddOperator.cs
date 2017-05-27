using System.Linq.Expressions;

namespace Orion.Scripting.Ast
{
    internal class AstAddOperator : AstOperator
    {
        public AstAddOperator(string value) : base(value) { }

        protected override Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right)
        {
            return Expression.Add(left, right);
        }
    }
}