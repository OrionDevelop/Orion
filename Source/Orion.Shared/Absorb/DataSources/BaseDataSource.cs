using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;

using Orion.Shared.Absorb.Objects;

namespace Orion.Shared.Absorb.DataSources
{
    public abstract class BaseDataSource : IDisposable
    {
        private readonly Dictionary<string, List<long>> _ids;
        private readonly object _lockObj;
        private readonly Dictionary<string, Subject<StatusBase>> _observers;
        private readonly Dictionary<string, bool> _shouldSendHeartbeat;
        private readonly List<string> _sources;
        protected Dictionary<string, IDisposable> Disposables { get; }
        protected ReadOnlyCollection<string> Sources => _sources.AsReadOnly();
        protected CancellationTokenSource CancellationToken { get; private set; }

        protected BaseDataSource()
        {
            _lockObj = new object();
            _ids = new Dictionary<string, List<long>>();
            _observers = new Dictionary<string, Subject<StatusBase>>();
            _shouldSendHeartbeat = new Dictionary<string, bool>();
            _sources = new List<string>();
            Disposables = new Dictionary<string, IDisposable>();
            CancellationToken = new CancellationTokenSource();
        }

        public void Dispose()
        {
            CancellationToken.Dispose();
            CancellationToken = null;
            foreach (var disposable in Disposables.Select(w => w.Value))
                disposable.Dispose();
            foreach (var observer in _observers.Select(w => w.Value))
                observer.Dispose();
        }

        protected abstract void Connect(Source source);

        protected abstract string NormalizedSource(string source);

        public IObservable<StatusBase> Connect(string source)
        {
            lock (_lockObj)
            {
                var sourceStr = NormalizedSource(source);
                if (_sources.Contains(sourceStr))
                    return _observers[sourceStr];

                _sources.Add(sourceStr);
                RegisterObserver(sourceStr);
                Connect(new Source {IsAdded = true, Name = sourceStr});
                return _observers[sourceStr];
            }
        }

        public void Disconnect(string sourceStr, bool clearIds = true)
        {
            var source = NormalizedSource(sourceStr);
            if (!_sources.Contains(source))
                return;
            _sources.Remove(source);

            if (_sources.Contains(source))
                return; // multiple connection to same source.

            Connect(new Source {IsAdded = false, Name = source});
            try
            {
                Disposables[source].Dispose();
            }
            catch
            {
                // CoreTweet throw.
            }
            Disposables.Remove(source);
            _observers[source].OnCompleted();
            _observers[source].Dispose();
            _observers.Remove(source);
            if (clearIds)
            {
                _ids.Remove(source);
                _shouldSendHeartbeat.Remove(source);
            }
        }

        protected bool IsConnected(string source)
        {
            return Disposables.ContainsKey(NormalizedSource(source));
        }

        protected void Heartbeat(string sourceStr)
        {
            var source = NormalizedSource(sourceStr);
            if (!_shouldSendHeartbeat[source])
                return;
            _observers[source].OnNext(new HeartbeatStatus());
            _shouldSendHeartbeat[source] = false;
        }

        protected void OnError(string sourceStr, Exception e)
        {
            var source = NormalizedSource(sourceStr);
            _observers[source].OnError(e);
            _shouldSendHeartbeat[source] = true;
            Disconnect(sourceStr, false);
        }

        protected void AddStatus(string sourceStr, StatusBase status)
        {
            var source = NormalizedSource(sourceStr);
            if (!status.IgnoreIdDuplication && _ids[source].Contains(status.Id))
                return;
            if (!status.IgnoreIdDuplication)
                _ids[source].Add(status.Id);
            _observers[source].OnNext(status);
        }

        private void RegisterObserver(string sourceStr)
        {
            var observer = new Subject<StatusBase>();
            var source = NormalizedSource(sourceStr);
            _observers.Add(source, observer);

            if (!_ids.ContainsKey(source))
                _ids.Add(source, new List<long>());
            if (!_shouldSendHeartbeat.ContainsKey(source))
                _shouldSendHeartbeat.Add(source, false);
        }
    }
}