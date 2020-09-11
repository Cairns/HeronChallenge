using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Heron.BO.Inventory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heron.IO
{
    public class BarcodeConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            ObservableCollection<Barcode> barcodes = new ObservableCollection<Barcode>();

            if (!String.IsNullOrWhiteSpace(text))
            {
                string[] barcodeTokens = text.Split('|');

                foreach (string barcode in barcodeTokens)
                {
                    barcodes.Add(new Barcode { Code = barcode });
                }
            }

            return barcodes;
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            string barcodeString = String.Empty;
            var barcodes = value as ICollection<Barcode>;

            if (barcodes != null)
            {
                barcodeString = String.Join("|", barcodes.Select(b => b.Code));
            }

            return barcodeString;
        }
    }
}
