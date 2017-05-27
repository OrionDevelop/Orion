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

        public bool Is(TokenType type)
        {
            return Type == type;
        }

        public bool Is(TokenType type, string value)
        {
            return Type == type && Value == value;
        }
    }
}