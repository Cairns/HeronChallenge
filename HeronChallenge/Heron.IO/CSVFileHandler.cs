using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heron.IO
{
    public class CSVFileHandler<T> : IFileHandler<T>
    {
        protected IList<ClassMap> ClassMaps { get; set; }

        public CSVFileHandler(IList<ClassMap> classMaps)
        {
            this.ClassMaps = classMaps;
        }

        public IEnumerable<T> Read<T>(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.Delimiter = ",";
                    csv.Configuration.HasHeaderRecord = true;
                    csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();

                    foreach (ClassMap map in this.ClassMaps)
                    {
                        csv.Configuration.RegisterClassMap(map);
                    }

                    var records = csv.GetRecords<T>().ToList();
                    return records;
                }
            }
        }

        public void Write<T>(IEnumerable<T> data, string fileName)
        {
            using (var writer = new StreamWriter(fileName))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    foreach (ClassMap map in this.ClassMaps)
                    {
                        csv.Configuration.RegisterClassMap(map);
                    }

                    csv.WriteRecords<T>(data);
                }
            }
        }

    }
}
