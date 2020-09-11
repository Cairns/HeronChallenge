using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Heron.IO
{
    public interface IFileHandler<T>
    {
        IEnumerable<T> Read<T>(string fileName);

        void Write<T>(IEnumerable<T> data, string fileName);
    }
}
