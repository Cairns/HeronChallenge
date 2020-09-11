using Heron.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Heron.UI.Common
{
    public class ViewModelBase : ValidatableBase
    {
        public IViewBase View { get; protected set; }

        public void DisplayError(string message)
        {
            this.View.DisplayError(message);
        }

        public void DisplayError(System.Exception ex)
        {
            this.View.DisplayError(ex);
        }
    }
}
