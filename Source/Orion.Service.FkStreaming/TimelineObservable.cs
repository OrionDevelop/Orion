using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orion.Service.FkStreaming
{
    public class TimelineObservable<T> : IObservable<T>
    {
        private readonly Func<T, Task<IEnumerable<T>>> _apiCall;

        public TimelineObservable(Func<T, Task<IEnumerable<T>>> apiCall)
        {
            _apiCall = apiCall;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            var connector = new TimelineConnection<T>(_apiCall, observer);
            connector.Connect();
            return connector;
        }
    }
}