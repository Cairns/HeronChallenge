using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for UserControlBase.xaml
    /// </summary>
    public class UserControlBase : UserControl, IViewBase
    {
        public UserControlBase()
        {
        }

        public void Close()
        {
            //Do nothing
        }

        public bool? DialogResult { get; set; }

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
    }
}


