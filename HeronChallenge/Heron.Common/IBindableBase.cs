using System.ComponentModel;

namespace Heron.Common
{
    public interface IBindableBase : INotifyPropertyChanged, IChangeTracking
    {
        bool SetProperty<T>(ref T storage, T value, string propertyName);
    }
}
