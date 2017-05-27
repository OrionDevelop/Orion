using System.Linq.Expressions;
using System.Reflection;

namespace Orion.Scripting.Ast
{
    internal class AstContainsOperator : AstOperator
    {
        public AstContainsOperator(string value) : base(value) { }

        protected override Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right)
        {
            var leftStr = ToStringExpression(left);
            var rightStr = ToStringExpression(right);

            return Expression.Call(leftStr, leftStr.Type.GetRuntimeMethod("Contains", new[] {typeof(string)}), rightStr);
        }
    }
}