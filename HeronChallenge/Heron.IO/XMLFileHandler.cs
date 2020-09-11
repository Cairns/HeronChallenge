using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Heron.IO
{
    public class XMLFileHandler<T> : IFileHandler<T>
    {
        public IEnumerable<T> Read<T>(string fileName)
        {
            IList<T> records = new List<T>();
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas()))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));

                    var record = (T)serializer.ReadObject(reader, true);
                    records.Add(record);
                }
            }
            return records;
        }

        public void Write<T>(IEnumerable<T> data, string fileName)
        {
            using (Stream stream = new FileStream(fileName, FileMode.Create))
            {
                foreach (T item in data)
                {
                    using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(stream, Encoding.UTF8))
                    {
                        DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                        serializer.WriteObject(writer, item);
                    }
                }
            }
        }
    }
}
