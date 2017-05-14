using System;

namespace Orion.Scripting.Parsing
{
    public static class QueryCompiler
    {
        public static FilterQuery Compile(string query)
        {
            var tokens = Tokenizer.Tokenize(query);

            throw new NotImplementedException();
        }
    }
}