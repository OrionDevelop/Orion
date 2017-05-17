using System.Collections.ObjectModel;

using Orion.Shared.Absorb.Objects;

namespace Orion.Shared.Absorb.DataSources
{
    public class BaseDataSource
    {
        private readonly ObservableCollection<StatusBase> _statuses;
        public ReadOnlyObservableCollection<StatusBase> Statuses => new ReadOnlyObservableCollection<StatusBase>(_statuses);

        public BaseDataSource()
        {
            _statuses = new ObservableCollection<StatusBase>();
        }
    }
}