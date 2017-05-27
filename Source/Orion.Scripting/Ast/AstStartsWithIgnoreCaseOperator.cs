using System.Linq.Expressions;
using System.Reflection;

namespace Orion.Scripting.Ast
{
    internal class AstStartsWithIgnoreCaseOperator : AstOperator
    {
        public AstStartsWithIgnoreCaseOperator(string value) : base(value) { }

        protected override Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right)
        {
            var leftStr = ToLowerExpression(ToStringExpression(left));
            var rightStr = ToLowerExpression(ToStringExpression(right));

            return Expression.Call(leftStr, leftStr.Type.GetRuntimeMethod("StartsWith", new[] {typeof(string)}), rightStr);
        }
    }
}