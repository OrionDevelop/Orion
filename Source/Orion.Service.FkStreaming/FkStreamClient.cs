using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orion.Service.FkStreaming
{
    public class FkStreamClient
    {
        public static TimeSpan TimeSpan { get; set; } = TimeSpan.FromSeconds(30);

        /// <summary>
        ///     Use non-streaming APIs as streaming API.
        /// </summary>
        /// <typeparam name="T">Response Type</typeparam>
        /// <param name="apiCall">API caller</param>
        /// <returns></returns>
        public static IObservable<T> AsObservable<T>(Func<T, Task<IEnumerable<T>>> apiCall)
        {
            return new TimelineObservable<T>(apiCall);
        }
    }
}