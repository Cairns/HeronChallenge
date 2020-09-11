using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Heron.IO
{
    public class JsonFileHandler<T> : IFileHandler<T>
    {
        public IEnumerable<T> Read<T>(string fileName)
        {
            IList<T> records = new List<T>();
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    using (JsonTextReader reader = new JsonTextReader(streamReader))
                    {
                        var serializer = new JsonSerializer();
                        records = serializer.Deserialize<IList<T>>(reader);
                    }
                }
            }
            return records;
        }

        public void Write<T>(IEnumerable<T> data, string fileName)
        {
            using (Stream stream = new FileStream(fileName, FileMode.Create))
            {
                using (StreamWriter streamWriter = new StreamWriter(stream))
                {
                    using (JsonTextWriter writer = new JsonTextWriter(streamWriter))
                    {
                        var serializer = new JsonSerializer();
                        serializer.Serialize(writer, data);
                    }
                }
            }
        }
    }
}
