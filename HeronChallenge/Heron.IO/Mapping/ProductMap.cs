using CsvHelper.Configuration;
using Heron.BO.Inventory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heron.IO.Mapping
{
    class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Map(m => m.ProductCode);
            Map(m => m.Description);
            Map(m => m.RetailPrice).Name("RetailPriceIncVat");
            Map(m => m.VatRate);
            Map(m => m.Barcodes).TypeConverter<BarcodeConverter<ObservableCollection<Barcode>>>();
        }
    }
}
