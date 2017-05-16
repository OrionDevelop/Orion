using System.Linq.Expressions;

namespace Orion.Scripting
{
    public abstract class FilterSourceBase
    {
        internal string KeyWithProvider => $"{Key}_{Provider}";
        public abstract string Key { get; }
        public abstract string Provider { get; }
    }
}