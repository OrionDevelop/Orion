using System;
using System.Linq.Expressions;

namespace Orion.Scripting.Ast
{
    internal class AstPrimary<T> : AstNode
    {
        public new T Value { get; }

        public AstPrimary(T value) : base(value.ToString())
        {
            Value = value;
        }

        public override Expression<Func<T1, bool>> EvaluateRootFunc<T1>()
        {
            return Expression.Lambda<Func<T1, bool>>(Expression.Constant(Value), Expression.Parameter(typeof(T1), "w"));
        }

        public override Expression EvaluateFunc<T1>(ParameterExpression parameter)
        {
            return Expression.Constant(Value);
        }
    }
}