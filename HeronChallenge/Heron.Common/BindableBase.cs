using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Heron.Common
{
    public class BindableBase : INotifyPropertyChanged, IChangeTracking
    {
        public virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }
            else
            {
                storage = value;
                OnPropertyChanged(propertyName);
                IsChanged = true;
                return true;
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region IChangeTracking Members
        public bool IsChanged { get; protected set; }

        public void AcceptChanges() => IsChanged = false;
        #endregion
    }
}
