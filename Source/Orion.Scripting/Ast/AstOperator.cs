using System;
using System.Linq.Expressions;

namespace Orion.Scripting.Ast
{
    internal class AstOperator : AstNode
    {
        public AstNode Left { get; set; }
        public AstNode Right { get; set; }

        public AstOperator(string value) : base(value) { }

        public override Expression<Func<T, bool>> EvaluateRootFunc<T>()
        {
            throw new NotImplementedException();
        }

        public override Expression EvaluateFunc<T>(ParameterExpression parameter)
        {
            throw new NotImplementedException();
        }
    }
}