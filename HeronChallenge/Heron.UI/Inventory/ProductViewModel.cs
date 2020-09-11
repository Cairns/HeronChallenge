using Heron.BO.Inventory;
using Heron.Common;
using Heron.IO;
using Heron.UI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Heron.UI.Inventory
{
    public class ProductViewModel : ViewModelBase, IProductViewModel
    {
        public ObservableCollection<Product> Products { get; set; }

        private Product _product;
        public Product Product
        {
            get => _product;
            set
            {
                SetProperty(ref _product, value);
                _product.PropertyChanged -= _product_PropertyChanged;
                _product.PropertyChanged += _product_PropertyChanged;
            }
        }

        private void _product_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SaveCommand.OnExecuteChanged();
        }

        private Barcode _selectedBarcode;
        public Barcode SelectedBarcode
        {
            get => _selectedBarcode;
            set
            {
                SetProperty(ref _selectedBarcode, value);
                this.AddCommand.OnExecuteChanged();
                this.RemoveCommand.OnExecuteChanged();
            }
        }

        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand RemoveCommand { get; set; }
        public DelegateCommand<ICloseable> CloseWindowCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public ProductViewModel()
            :base()
        {
            AddCommand = new DelegateCommand(OnAdd);
            RemoveCommand = new DelegateCommand(OnRemove, CanRemove);

            CloseWindowCommand = new DelegateCommand<ICloseable>(CloseWindow);
            SaveCommand = new DelegateCommand(OnSave);
            CancelCommand = new DelegateCommand(OnCancel);
        }

        public ProductViewModel(IProductView view)
            : this()
        {
            this.View = view;

            Product = new Product();
        }

        public ProductViewModel(IProductView view, Product product)
            : this(view)
        {
            Product = product;
        }

        private void CloseWindow(ICloseable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        public void OnAdd()
        {
            //Add new barcode
        }

        public void OnRemove()
        {
            //Remove selected barcode
            this.Product.Barcodes.Remove(this.SelectedBarcode);
            this.SelectedBarcode = null;
        }

        public bool CanRemove()
        {
            //Do we have a selected barcode
            return this.SelectedBarcode != null;
        }

        private void OnSave()
        {
            try
            {
                this.Product.Validate();
                if (this.Product.HasErrors)
                {
                    base.DisplayError(this.Product.ErrorMessage);
                    return;
                }

                this.View.DialogResult = true;
                this.CloseWindow(this.View);
            }
            catch(System.Exception ex)
            {
                base.DisplayError(ex);
            }
        }

        private void OnCancel()
        {
            this.View.DialogResult = false;
            this.CloseWindow(this.View);
        }
    }
}
