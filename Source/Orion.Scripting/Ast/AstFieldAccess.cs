using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Orion.Scripting.Ast
{
    internal class AstFieldAccess : AstNode
    {
        public AstFieldAccess(string value) : base(value) { }

        // (w) => w.`Value`
        public override Expression<Func<T, bool>> EvaluateRootFunc<T>()
        {
            var parameter = Expression.Parameter(typeof(T), "w");
            return Expression.Lambda<Func<T, bool>>(FieldAccess<T>(parameter), parameter);
        }

        public override Expression EvaluateFunc<T>(ParameterExpression expression)
        {
            return FieldAccess<T>(expression);
        }

        private Expression FieldAccess<T>(ParameterExpression parameter)
        {
            if (!Value.Contains("."))
                return Expression.MakeMemberAccess(parameter, typeof(T).GetRuntimeProperty(Value));

            Expression lastAccess = null;
            PropertyInfo lastProperty = null;
            foreach (var property in Value.Split('.'))
                if (lastAccess == null)
                {
                    lastProperty = typeof(T).GetRuntimeProperty(property);
                    lastAccess = Expression.MakeMemberAccess(parameter, lastProperty);
                }
                else
                {
                    lastAccess = Expression.MakeMemberAccess(lastAccess, lastProperty.PropertyType.GetRuntimeProperty(property));
                    lastProperty = lastProperty.PropertyType.GetRuntimeProperty(property);
                }
            return lastAccess;
        }
    }
}