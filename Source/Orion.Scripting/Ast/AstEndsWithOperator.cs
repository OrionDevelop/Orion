using System.Linq.Expressions;
using System.Reflection;

namespace Orion.Scripting.Ast
{
    internal class AstEndsWithOperator : AstOperator
    {
        public AstEndsWithOperator(string value) : base(value) { }

        protected override Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right)
        {
            var leftStr = ToStringExpression(left);
            var rightStr = ToStringExpression(right);

            return Expression.Call(leftStr, leftStr.Type.GetRuntimeMethod("EndsWith", new[] {typeof(string)}), rightStr);
        }
    }
}