using Heron.BO.Inventory;
using Heron.Common;
using Heron.UI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heron.UI.Inventory
{
    public interface IProductViewModel : IViewModelBase
    {
        ObservableCollection<Product> Products { get; set; }

        Product Product { get; set; }

        DelegateCommand SaveCommand { get; set; }
        DelegateCommand CancelCommand { get; set; }
    }
}
