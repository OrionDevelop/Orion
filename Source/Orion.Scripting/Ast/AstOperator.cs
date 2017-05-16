namespace Orion.Scripting.Ast
{
    internal class AstOperator : AstNode
    {
        public AstNode Left { get; set; }
        public AstNode Right { get; set; }

        public AstOperator(string value) : base(value) { }
    }
}