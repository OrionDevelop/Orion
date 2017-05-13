using System;

namespace Orion.Scripting.Parsing
{
    internal class QueryParsingException : Exception
    {
        public QueryParsingException(string message) : base($"解析エラー: {message}") { }
    }
}