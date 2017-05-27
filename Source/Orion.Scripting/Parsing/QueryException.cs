using System;

namespace Orion.Scripting.Parsing
{
    internal class QueryException : Exception
    {
        public QueryException(string message) : base($"コンパイルエラー: {message}") { }
    }
}