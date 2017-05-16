namespace Orion.Scripting.Ast
{
    public abstract class AstNode
    {
        public string Value { get; }

        protected AstNode(string value)
        {
            Value = value;
        }
    }
}