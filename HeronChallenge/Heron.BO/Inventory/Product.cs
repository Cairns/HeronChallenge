using Heron.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Heron.BO.Inventory
{
    public class Product : ValidatableBase
    {
        private string _productCode;
        
        [StringLength(5, MinimumLength = 5, ErrorMessage = "{0} can have a max of {1} characters")]
        [MaxLength(5, ErrorMessage = "{0} can have a max of {1} characters")]
        [Required]
        public string ProductCode { get => _productCode; set => SetProperty(ref _productCode, value); }

        private string _description;
        [Required]
        public string Description { get => _description; set => SetProperty(ref _description, value); }

        private decimal _retailPrice;
        [Required]
        public decimal RetailPrice { get => _retailPrice; set => SetProperty(ref _retailPrice, value); }

        private decimal _vatRate;
        public decimal VatRate { get => _vatRate; set => SetProperty(ref _vatRate, value); }

        private ObservableCollection<Barcode> _barcodes;
        public ObservableCollection<Barcode> Barcodes { get => _barcodes; set => SetProperty(ref _barcodes, value); }
    }
}
