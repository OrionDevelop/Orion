using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Orion.Scripting.Ast
{
    internal abstract class AstOperator : AstNode
    {
        public AstNode Left { get; set; }
        public AstNode Right { get; set; }

        public AstOperator(string value) : base(value) { }

        public override Expression<Func<T, bool>> EvaluateRootFunc<T>()
        {
            var parameter = Expression.Parameter(typeof(T), "w");
            return Expression.Lambda<Func<T, bool>>(CreateExpression<T>(parameter), parameter);
        }

        public override Expression EvaluateFunc<T>(ParameterExpression parameter)
        {
            return CreateExpression<T>(parameter);
        }

        private Expression CreateExpression<T>(ParameterExpression parameter)
        {
            var left = Left.EvaluateFunc<T>(parameter);
            var right = Right.EvaluateFunc<T>(parameter);
            return CreateExpression<T>(parameter, left, right);
        }

        protected abstract Expression CreateExpression<T>(ParameterExpression parameter, Expression left, Expression right);

        protected Expression ToStringExpression(Expression expr)
        {
            return Expression.Call(expr, expr.Type.GetRuntimeMethod("ToString", new Type[] {}));
        }

        protected Expression ToLowerExpression(Expression expr)
        {
            return Expression.Call(expr, expr.Type.GetRuntimeMethod("ToLower", new Type[] {}));
        }
    }
}