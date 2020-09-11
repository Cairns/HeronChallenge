using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heron.UI.Common
{
    public interface IViewBase : ICloseable
    {
        bool? DialogResult { get; set; }

        void DisplayError(string message);

        void DisplayError(System.Exception ex);
    }
}
