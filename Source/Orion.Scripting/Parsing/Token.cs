namespace Orion.Scripting.Parsing
{
    public class Token
    {
        public string Value { get; }
        public TokenType Type { get; }

        public Token(TokenType type, string value)
        {
            Value = value;
            Type = type;
        }
    }
}