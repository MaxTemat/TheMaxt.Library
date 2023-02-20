using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TheMaxt.Serialization
{
    /// <summary>
    /// This class allows to serialize and deserialize the data according to the binary format
    /// </summary>
    public static class Binary
    {
        /// <summary>
        /// This method allows to serialize an object according to the binary format
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <param name="path">The path to the backup file of the serialized object</param>
        public static void Serialize(object obj, string path)
        {
            using (Stream file = File.OpenWrite(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, obj);
            }
        }

        /// <summary>
        /// This method allows to deserialize an object according to the binary format
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize</typeparam>
        /// <param name="path">The path to the backup file of the serialized object</param>
        /// <returns>Returns an object of type <typeparamref name="T"/></returns>
        public static T Deserialize<T>(string path)
        {
            using (Stream file = File.Open(path, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(file);
            }
        }
    }
}
