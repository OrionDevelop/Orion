using System;

using Orion.Scripting.Ast;

namespace Orion.Scripting
{
    public sealed class FilterQuery
    {
        public FilterSourceBase Source { get; internal set; }
        public string SourceStr { get; internal set; }

        public Delegate Delegate { get; internal set; }

        public AstNode DebugInfo { get; internal set; }
    }
}