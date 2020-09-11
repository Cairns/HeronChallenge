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
    public interface IProductsViewModel : IViewModelBase
    {
        ObservableCollection<Product> Products { get; set; }
        Product SelectedProduct { get; set; }

        DelegateCommand LoadCommand { get; set; }
        DelegateCommand SaveCommand { get; set; }

        DelegateCommand AddCommand { get; set; }
        DelegateCommand EditCommand { get; set; }
        DelegateCommand RemoveCommand { get; set; }
    }
}
