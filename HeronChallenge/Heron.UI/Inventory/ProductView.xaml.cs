using Heron.BO.Inventory;
using Heron.UI.Common;
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

namespace Heron.UI.Inventory
{
    /// <summary>
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : WindowBase, IProductView
    {
        public ProductView()
        {
            InitializeComponent();

            this.DataContext = new ProductViewModel(this);
        }

        public ProductView(Product product)
        {
            InitializeComponent();

            this.DataContext = new ProductViewModel(this, product);
        }
    }
}
