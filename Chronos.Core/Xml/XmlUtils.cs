using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Chronos.Core.Xml
{
    public static class XmlUtils
    {
        #region Properties

        private static readonly Dictionary<Type, XmlSerializer> Serializers = new Dictionary<Type, XmlSerializer>();

        #endregion

        #region Serialize

        /// <summary>
        ///   Serializes the specified file name.
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "fileName">Name of the file.</param>
        /// <param name = "item">The item.</param>
        public static void Serialize<T>(string fileName, T item)
        {
            using (var writer = new StreamWriter(fileName))
            {
                if (!Serializers.ContainsKey(typeof(T)))
                    Serializers.Add(typeof(T), new XmlSerializer(typeof(T)));

                Serializers[typeof(T)].Serialize(writer, item);
            }
        }

        /// <summary>
        ///   Serializes the specified stream.
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "stream">The stream.</param>
        /// <param name = "item">The item.</param>
        public static void Serialize<T>(Stream stream, T item)
        {
            using (var writer = new StreamWriter(stream))
            {
                if (!Serializers.ContainsKey(typeof(T)))
                    Serializers.Add(typeof(T), new XmlSerializer(typeof(T)));

                Serializers[typeof(T)].Serialize(writer, item);
            }
        }

        /// <summary>
        ///   Serializes the specified file name.
        /// </summary>
        /// <param name = "fileName">Name of the file.</param>
        /// <param name = "item">The item.</param>
        public static void Serialize(string fileName, object item, Type valueType)
        {
            using (var writer = new StreamWriter(fileName))
            {
                if (!Serializers.ContainsKey(valueType))
                    Serializers.Add(valueType, new XmlSerializer(valueType));

                Serializers[valueType].Serialize(writer, item);
            }
        }

        /// <summary>
        ///   Serializes the specified stream.
        /// </summary>
        /// <param name = "stream">The stream.</param>
        /// <param name = "item">The item.</param>
        public static void Serialize(Stream stream, object item, Type valueType)
        {
            using (var writer = new StreamWriter(stream))
            {
                if (!Serializers.ContainsKey(valueType))
                    Serializers.Add(valueType, new XmlSerializer(valueType));

                Serializers[valueType].Serialize(writer, item);
            }
        }

        #endregion

        #region Deserialize

        /// <summary>
        ///   Deserializes the specified file name.
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "fileName">Name of the file.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                if (!Serializers.ContainsKey(typeof(T)))
                    Serializers.Add(typeof(T), new XmlSerializer(typeof(T)));

                return (T)Serializers[typeof(T)].Deserialize(reader);
            }
        }

        /// <summary>
        ///   Deserializes the specified stream.
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "stream">The stream.</param>
        /// <returns></returns>
        public static T Deserialize<T>(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                if (!Serializers.ContainsKey(typeof(T)))
                    Serializers.Add(typeof(T), new XmlSerializer(typeof(T)));

                return (T)Serializers[typeof(T)].Deserialize(reader);
            }
        }
        
        /// <summary>
        ///   Deserializes the specified stream.
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "stream">The stream.</param>
        /// <returns></returns>
        public static T Deserialize<T>(StringReader reader)
        {
            if (!Serializers.ContainsKey(typeof(T)))
                Serializers.Add(typeof(T), new XmlSerializer(typeof(T)));

            return (T)Serializers[typeof(T)].Deserialize(reader);
        }

        /// <summary>
        ///   Deserializes the specified file name.
        /// </summary>
        /// <param name = "fileName">Name of the file.</param>
        /// <returns></returns>
        public static object Deserialize(string fileName, Type valueType)
        {
            using (var reader = new StreamReader(fileName))
            {
                if (!Serializers.ContainsKey(valueType))
                    Serializers.Add(valueType, new XmlSerializer(valueType));

                return Serializers[valueType].Deserialize(reader);
            }
        }

        /// <summary>
        ///   Deserializes the specified stream.
        /// </summary>
        /// <param name = "stream">The stream.</param>
        /// <returns></returns>
        public static object Deserialize(Stream stream, Type valueType)
        {
            using (var reader = new StreamReader(stream))
            {
                if (!Serializers.ContainsKey(valueType))
                    Serializers.Add(valueType, new XmlSerializer(valueType));

                return Serializers[valueType].Deserialize(reader);
            }
        }
        #endregion
    }
}