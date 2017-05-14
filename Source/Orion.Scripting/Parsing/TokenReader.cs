using System.Collections.Generic;

using Orion.Scripting.Expressions;

namespace Orion.Scripting.Parsing
{
    public class TokenReader
    {
        private readonly List<Token> _tokens;
        private int _cursor;

        // ReSharper disable once InconsistentNaming
        public bool IsEOT => _cursor >= _tokens.Count;

        public TokenReader(IEnumerable<Token> tokens)
        {
            _tokens = new List<Token>(tokens);
            _cursor = 0;
        }

        public Token Read()
        {
            return _tokens[_cursor++];
        }

        public Token LookAhead()
        {
            if (IsEOT)
                throw new QueryException("構文解析中にファイルの終わりに達しました。");
            return _tokens[_cursor];
        }
    }
}