using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orion.Service.FkStreaming
{
    public class TimelineObservable<T> : IObservable<T>
    {
        private readonly Func<T, Task<IEnumerable<T>>> _apiCall;
        private readonly TimeSpan _timeSpan;

        public TimelineObservable(Func<T, Task<IEnumerable<T>>> apiCall, TimeSpan timeSpan)
        {
            _apiCall = apiCall;
            _timeSpan = timeSpan;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            var connector = new TimelineConnection<T>(_apiCall, observer, _timeSpan);
            connector.Connect();
            return connector;
        }
    }
}