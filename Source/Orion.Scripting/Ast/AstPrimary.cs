namespace Orion.Scripting.Ast
{
    internal class AstPrimary<T> : AstNode
    {
        public new T Value { get; }

        public AstPrimary(T value) : base(value.ToString())
        {
            Value = value;
        }
    }
}