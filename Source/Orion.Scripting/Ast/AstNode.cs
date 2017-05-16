using System;
using System.Linq.Expressions;

namespace Orion.Scripting.Ast
{
    public abstract class AstNode
    {
        public string Value { get; }

        protected AstNode(string value)
        {
            Value = value;
        }

        /// <summary>
        ///     ルート要素を作成します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public abstract Expression<Func<T, bool>> EvaluateRootFunc<T>();

        /// <summary>
        ///     要素を作成します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public abstract Expression EvaluateFunc<T>(ParameterExpression parameter);
    }
}