using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heron.UI.Common
{
    public interface IViewModelBase
    {
        IViewBase View { get; }

        void DisplayError(string message);

        void DisplayError(System.Exception ex);
    }
}
