using CsvHelper.Configuration;
using Heron.BO.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heron.IO.Mapping
{
    public class BarcodeMap : ClassMap<Barcode>
    {
        public BarcodeMap()
        {
            Map(m => m.Code);
        }
    }
}
