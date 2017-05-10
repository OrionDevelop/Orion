using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Orion.Service.Shared.Extensions
{
    public static class TaskEx
    {
        public static ConfiguredTaskAwaitable<T> Stay<T>(this Task<T> obj)
        {
            return obj.ConfigureAwait(false);
        }
    }
}