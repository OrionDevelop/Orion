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
        private readonly List<string> _sources;
        protected Dictionary<string, IDisposable> Disposables { get; }
        protected ReadOnlyCollection<string> Sources => _sources.AsReadOnly();
        protected CancellationTokenSource CancellationToken { get; private set; }

        protected BaseDataSource()
        {
            _lockObj = new object();
            _ids = new Dictionary<string, List<long>>();
            _sources = new List<string>();
            _observers = new Dictionary<string, Subject<StatusBase>>();
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
            var sourceStr = NormalizedSource(source);
            if (_sources.Contains(sourceStr))
                return _observers[sourceStr];

            _sources.Add(sourceStr);
            RegisterObserver(sourceStr);
            Connect(new Source {IsAdded = true, Name = sourceStr});
            return _observers[sourceStr];
        }

        public void Disconnect(string source, bool clearIds = true)
        {
            var sourceStr = NormalizedSource(source);
            if (!_sources.Contains(sourceStr))
                return;
            _sources.Remove(sourceStr);

            if (_sources.Contains(sourceStr))
                return; // multiple connection to same source.

            Connect(new Source {IsAdded = false, Name = sourceStr});
            Disposables[sourceStr].Dispose();
            Disposables.Remove(sourceStr);
            _observers[sourceStr].OnCompleted();
            _observers[sourceStr].Dispose();
            _observers.Remove(sourceStr);
            if (clearIds)
                _ids.Remove(sourceStr);
        }

        protected bool IsConnected(string source)
        {
            return Disposables.ContainsKey(NormalizedSource(source));
        }

        protected void OnError(string sourceStr, Exception e)
        {
            lock (_lockObj)
            {
                _observers[NormalizedSource(sourceStr)].OnError(e);
                Disconnect(sourceStr, false);
            }
        }

        protected void AddStatus(string sourceStr, StatusBase status)
        {
            lock (_lockObj)
            {
                if (_ids[NormalizedSource(sourceStr)].Contains(status.Id))
                    return;
                _ids[NormalizedSource(sourceStr)].Add(status.Id);
                _observers[NormalizedSource(sourceStr)].OnNext(status);
            }
        }

        private void RegisterObserver(string source)
        {
            var observer = new Subject<StatusBase>();
            _observers.Add(NormalizedSource(source), observer);
            if (!_ids.ContainsKey(NormalizedSource(source)))
                _ids.Add(NormalizedSource(source), new List<long>());
        }
    }
}