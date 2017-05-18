using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

using Orion.Shared.Absorb.Objects;

namespace Orion.Shared.Absorb.DataSources
{
    public abstract class BaseDataSource : IDisposable
    {
        private readonly List<long> _ids;
        private readonly object _lockObj;
        private readonly IObservable<StatusBase> _observable;
        private readonly Subject<StatusBase> _observer;
        private readonly List<string> _sources;
        protected ReadOnlyCollection<string> Sources => _sources.AsReadOnly();
        protected CancellationTokenSource CancellationToken { get; private set; }

        protected BaseDataSource()
        {
            _lockObj = new object();
            _ids = new List<long>();
            _sources = new List<string>();
            _observer = new Subject<StatusBase>();
            _observable = _observer.AsObservable();
            CancellationToken = new CancellationTokenSource();
        }

        public void Dispose()
        {
            CancellationToken.Dispose();
            CancellationToken = null;
            _observer.OnCompleted();
        }

        protected abstract void UpdateConnection();

        public IObservable<StatusBase> MergeSource(string source)
        {
            if (_sources.Contains(source))
                return _observable;

            _sources.Add(source);
            UpdateConnection();
            return _observable;
        }

        public void DisposeSource(string source)
        {
            if (!_sources.Contains(source))
                return;
            _sources.Remove(source);
            UpdateConnection();
        }

        protected void AddStatus(StatusBase status)
        {
            lock (_lockObj)
            {
                if (_ids.Contains(status.Id))
                    return;
                _ids.Add(status.Id);
                _observer.OnNext(status);
            }
        }

        protected IObservable<StatusBase> Merge(Func<Task<IEnumerable<StatusBase>>> firstAction, Func<IObservable<StatusBase>> streamAction)
        {
            return Observable.Create<StatusBase>(async observer =>
            {
                try
                {
                    var statuses = await firstAction.Invoke();
                    foreach (var status in statuses.Reverse())
                        observer.OnNext(status);
                    observer.OnCompleted();
                }
                catch (Exception e)
                {
                    observer.OnError(e);
                }
            }).Concat(streamAction.Invoke());
        }
    }
}