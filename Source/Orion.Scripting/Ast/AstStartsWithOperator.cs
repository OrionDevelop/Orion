using System.Linq.Expressions;
using System.Reflection;

namespace Orion.Scripting.Ast
{
    internal class AstStartsWithOperator : AstOperator
    {
        public AstStartsWithOperator(string value) : base(value) { }

        protected override Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right)
        {
            var leftStr = ToStringExpression(left);
            var rightStr = ToStringExpression(right);

            return Expression.Call(leftStr, leftStr.Type.GetRuntimeMethod("StartsWith", new[] {typeof(string)}), rightStr);
        }
    }
}