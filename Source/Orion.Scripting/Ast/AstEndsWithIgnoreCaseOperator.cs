using System.Linq.Expressions;
using System.Reflection;

namespace Orion.Scripting.Ast
{
    internal class AstEndsWithIgnoreCaseOperator : AstOperator
    {
        public AstEndsWithIgnoreCaseOperator(string value) : base(value) { }

        protected override Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right)
        {
            var leftStr = ToLowerExpression(ToStringExpression(left));
            var rightStr = ToLowerExpression(ToStringExpression(right));

            return Expression.Call(leftStr, leftStr.Type.GetRuntimeMethod("EndsWith", new[] {typeof(string)}), rightStr);
        }
    }
}