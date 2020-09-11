using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Heron.UI.Common
{
    /// <summary>
    /// Interaction logic for WindowBase.xaml
    /// </summary>
    public class WindowBase : Window, IDisposable, IViewBase
    {
        private bool disposedValue;

        public WindowBase()
        {
        }

        public new void Close()
        {
            base.Close();
        }

        public new bool? DialogResult { get; set; }

        public void DisplayError(string message)
        {
            MessageBox.Show(
                messageBoxText: message,
                caption: "Error",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error
                );
        }

        public void DisplayError(System.Exception ex)
        {
            MessageBox.Show(
                messageBoxText: ex.Message,
                caption: "Error",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error
                );
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ViewBase()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

