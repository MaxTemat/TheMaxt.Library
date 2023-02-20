using System.IO;
using System.Xml.Serialization;

namespace TheMaxt.Serialization
{
    /// <summary>
    /// This class allows to serialize and deserialize the data according to the xml format
    /// </summary>
    public static class Xml
    {
        /// <summary>
        /// This method allows to serialize an object according to the xml format
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="obj">Object to serialize</param>
        /// <returns>Returns a string that represents the xml data</returns>
        public static string Serialize<T>(object obj)
        {
            var path = "temp.tm";
            using (Stream file = File.OpenWrite(path))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                xs.Serialize(file, obj);
            }
            var text = File.ReadAllText(path);
            File.Delete(path);
            return text;
        }

        /// <summary>
        /// This method allows to serialize an object according to the xml format
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="path">The path to the backup file of the serialized object</param>
        /// <param name="obj">Object to serialize</param>
        public static void Serialize<T>(object obj, string path)
        {
            using (Stream file = File.OpenWrite(path))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                xs.Serialize(file, obj);
            }
        }

        /// <summary>
        /// This method allows to deserialize an object according to the xml format
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize</typeparam>
        /// <param name="xml">The xml string to deserialize</param>
        /// <returns>Returns an object of type <typeparamref name="T"/></returns>
        public static T DeserializeFromString<T>(string xml)
        {
            var path = "temp.tm";
            File.WriteAllText(path, xml);
            T obj;
            using (Stream file = File.Open(path, FileMode.Open))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                obj = (T)xs.Deserialize(file);
            }
            File.Delete(path);
            return obj;
        }

        /// <summary>
        /// This method allows to deserialize an object according to the xml format
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize</typeparam>
        /// <param name="path">The path to the backup file of the serialized object</param>
        /// <returns>Returns an object of type <typeparamref name="T"/></returns>
        public static T Deserialize<T>(string path)
        {
            using (Stream file = File.Open(path, FileMode.Open))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                return (T)xs.Deserialize(file);
            }
        }
    }
}
