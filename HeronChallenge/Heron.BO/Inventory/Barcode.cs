using Heron.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heron.BO.Inventory
{
    public class Barcode : ValidatableBase
    {
        private string _code;
        public string Code { get => _code; set => SetProperty(ref _code, value); }
    }
}
