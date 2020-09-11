using CsvHelper.Configuration;
using Heron.BO.Inventory;
using Heron.IO.Common;
using Heron.IO.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Heron.IO
{
    public static class FileHandlerFactory<T>
    {
        private static IList<ClassMap> registeredClassMaps = null;
        private static Dictionary<string, IFileHandler<T>> registeredFileHandlers = null;

        private static void RegisterClassMaps()
        {
            if (registeredClassMaps != null)
            {
                return;
            }
            else
            {
                registeredClassMaps = new List<ClassMap>();
                registeredClassMaps.Add(new ProductMap());
            }
        }

        private static void RegisterFileHandlers()
        {
            if (registeredFileHandlers != null)
            {
                return;
            }
            else
            {
                registeredFileHandlers = new Dictionary<string, IFileHandler<T>>();
                registeredFileHandlers.Add(FileType.CSV, new CSVFileHandler<T>(registeredClassMaps));
                registeredFileHandlers.Add(FileType.XML, new XMLFileHandler<T>());
                registeredFileHandlers.Add(FileType.Json, new JsonFileHandler<T>());
            }
        }

        public static IFileHandler<T> Create(string fileType)
        {
            RegisterClassMaps();
            RegisterFileHandlers();

            IFileHandler<T> fileHandler = null;

            if (registeredFileHandlers.ContainsKey(fileType))
            {
                fileHandler = registeredFileHandlers[fileType];
            }

            return fileHandler;
        }
    }
}
