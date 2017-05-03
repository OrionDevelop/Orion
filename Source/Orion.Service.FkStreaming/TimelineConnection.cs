using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Orion.Service.FkStreaming
{
    public class TimelineConnection<T> : IDisposable
    {
        private readonly Func<T, Task<IEnumerable<T>>> _apiCall;
        private T _lastObject;
        private IObserver<T> _observer;
        private CancellationTokenSource _tokenSource;

        public TimelineConnection(Func<T, Task<IEnumerable<T>>> apiCall, IObserver<T> observable)
        {
            _apiCall = apiCall;
            _observer = observable;
            _tokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _tokenSource.Dispose();
            _tokenSource = null;
            _observer.OnCompleted();
            _observer = null;
        }

        internal void Connect()
        {
            try
            {
                Exception e = null; //
                Task.Run(async () =>
                {
                    while (true)
                        try
                        {
                            var response = (await _apiCall.Invoke(_lastObject)).ToList();
                            if (response.Any())
                            {
                                _lastObject = response.First();
                                response.Reverse();
                                foreach (var obj in response)
                                    _observer.OnNext(obj);
                            }
                            if (_tokenSource.Token.IsCancellationRequested)
                                break;

                            Task.Delay(FkStreamClient.TimeSpan).Wait();
                        }
                        catch (Exception ie)
                        {
                            e = ie;
                            break;
                        }
                }, _tokenSource.Token);
                if (e != null)
                    throw e;
            }
            catch (Exception e)
            {
                _observer.OnError(e);
            }
        }
    }
}