using System;
using System.IO;
using System.Text.Json;

namespace TheMaxt.Serialization.Standard
{
    /// <summary>
    /// This class allows to serialize and deserialize the data according to the json format
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// This method allows to serialize an object according to the json format
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="obj">Object to serialize</param>
        /// <returns>Returns a string that represents the json data</returns>
        public static string Serialize<T>(object obj)
        {
            var path = "temp.tm";
            using (Stream file = File.OpenWrite(path))
            {
                Utf8JsonWriter utf = new Utf8JsonWriter(file);
                JsonSerializer.Serialize(utf, obj, typeof(T));
            }
            var text = File.ReadAllText(path);
            File.Delete(path);
            return text;
        }

        /// <summary>
        /// This method allows to serialize an object according to the json format
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="path">The path to the backup file of the serialized object</param>
        /// <param name="obj">Object to serialize</param>
        public static void Serialize<T>(object obj, string path)
        {
            using (Stream file = File.OpenWrite(path))
            {
                Utf8JsonWriter utf = new Utf8JsonWriter(file);
                JsonSerializer.Serialize(utf, obj, typeof(T));
            }
        }

        /// <summary>
        /// This method allows to deserialize an object according to the json format
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize</typeparam>
        /// <param name="json">The json string to deserialize</param>
        /// <returns>Returns an object of type <typeparamref name="T"/></returns>
        public static T DeserializeFromString<T>(string json)
        {
            var path = "temp.tm";
            File.WriteAllText(path, json);
            T obj;
            using (StreamReader sr = new StreamReader(path))
            {
                string text = "";
                while (!sr.EndOfStream)
                {
                    text += sr.ReadLine();
                }
                obj = (T)JsonSerializer.Deserialize(text, typeof(T));
            }
            File.Delete(path);
            return obj;
        }

        /// <summary>
        /// This method allows to deserialize an object according to the json format
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize</typeparam>
        /// <param name="path">The path to the backup file of the serialized object</param>
        /// <returns>Returns an object of type <typeparamref name="T"/></returns>
        public static T Deserialize<T>(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string json = "";
                while (!sr.EndOfStream)
                {
                    json += sr.ReadLine();
                }
                return (T)JsonSerializer.Deserialize(json, typeof(T));
            }
        }
    }
}
