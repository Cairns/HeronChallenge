using Heron.BO.Inventory;
using Heron.Common;
using Heron.IO;
using Heron.UI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Heron.UI.Inventory
{
    public class ProductsViewModel : ViewModelBase, IProductsViewModel
    {
        public DelegateCommand LoadCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }

        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand RemoveCommand { get; set; }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get
            {
                if (_products == null)
                {
                    _products = new ObservableCollection<Product>();
                }
                return _products;
            }
            set
            {
                SetProperty(ref _products, value);
                SaveCommand.OnExecuteChanged();
            }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                SetProperty(ref _selectedProduct, value);
                EditCommand.OnExecuteChanged();
                RemoveCommand.OnExecuteChanged();
            }
        }

        public ProductsViewModel()
            :base()
        {
        }

        public ProductsViewModel(IProductsView view)
            : base()
        {
            this.View = view;

            LoadCommand = new DelegateCommand(OnLoad);
            SaveCommand = new DelegateCommand(OnSave, CanSave);
            AddCommand = new DelegateCommand(OnAdd);
            EditCommand = new DelegateCommand(OnEdit, CanEdit);
            RemoveCommand = new DelegateCommand(OnRemove, CanRemove);
        }

        public void LoadProducts()
        {
            string fileName = this.GetProductsFileName();

            if (!String.IsNullOrWhiteSpace(fileName))
            {
                //Load file
                this.Products = new ObservableCollection<Product>(ReadFile(fileName));
            }
        }

        private string GetProductsFileName()
        {
            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }

            return String.Empty;
        }

        private IEnumerable<Product> ReadFile(string fileName)
        {
            IEnumerable<Product> products = null;

            try
            {
                var fileType = ParseReadFileName(fileName);
                var fileHandler = FileHandlerFactory<Product>.Create(fileType);
                products = fileHandler.Read<Product>(fileName);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return products;
        }

        private string ParseReadFileName(string fileName)
        {
            if (String.IsNullOrWhiteSpace(fileName))
            {
                throw new System.IO.FileNotFoundException("No file name specified");
            }
            else if (!File.Exists(fileName))
            {
                throw new System.IO.FileNotFoundException("Specified file does not exist");
            }
            else
            {
                return System.IO.Path.GetExtension(fileName).Replace(".", "");
            }
        }

        public void SaveProducts()
        {
            string fileName = this.SetProductsFileName();

            if (!String.IsNullOrWhiteSpace(fileName))
            {
                //Save file
                this.SaveFile(fileName);
            }
        }

        private string SetProductsFileName()
        {
            using (System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog())
            {
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return saveFileDialog.FileName;
                }
            }

            return String.Empty;
        }

        private void SaveFile(string fileName)
        {
            try
            {
                var fileType = ParseWriteFileName(fileName);
                var fileHandler = FileHandlerFactory<Product>.Create(fileType);
                fileHandler.Write(this.Products, fileName);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static string ParseWriteFileName(string fileName)
        {
            if (String.IsNullOrWhiteSpace(fileName))
            {
                throw new System.IO.FileNotFoundException("No file name specified");
            }
            else
            {
                return Path.GetExtension(fileName).Replace(".", "");
            }
        }

        public void OnLoad()
        {
            try
            {
                this.LoadProducts();
            }
            catch (System.Exception ex)
            {
                DisplayError(ex);
            }
        }

        public void OnSave()
        {
            try
            {
                this.SaveProducts();
            }
            catch(System.Exception ex)
            {
                base.DisplayError(ex);
            }
        }

        public bool CanSave()
        {
            //Do we have products to save?
            return this.Products != null && this.Products.Count > 0;
        }

        public void OnAdd()
        {
            try
            {
                //Add new product
                ProductView view = new ProductView();

                var result = view.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    var viewModel = view.DataContext as IProductViewModel;
                    if (viewModel != null)
                    {
                        this.Products.Add(viewModel.Product);
                    }
                }
            }
            catch(System.Exception ex)
            {
                base.DisplayError(ex);
            }
        }

        public void OnEdit()
        {
            try
            {
                //Edit existing product
                ProductView view = new ProductView(this.SelectedProduct);
                var result = view.ShowDialog();
            }
            catch (System.Exception ex)
            {
                base.DisplayError(ex);
            }
        }

        public bool CanEdit()
        {
            //Do we have a selected product
            return this.SelectedProduct != null;
        }

        public void OnRemove()
        {
            try
            {
                this.Products.Remove(SelectedProduct);
                this.SelectedProduct = null;
            }
            catch(System.Exception ex)
            {
                base.DisplayError(ex);
            }
        }

        public bool CanRemove()
        {
            //Do we have a selected product
            return this.SelectedProduct != null;
        }
    }
}
