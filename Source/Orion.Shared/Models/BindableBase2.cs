using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Orion.Shared.Models
{
    // Prism-based BindableBase class.
    public class BindableBase2 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T obj, T value, [CallerMemberName] string propertyName = null)
        {
            if (obj.Equals(value))
                return false;
            obj = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }
    }
}