using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Chronos.Core.Extensions
{
    public static class FormatterExtensions
    {
        public static byte[] ToBinary(this object obj)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.All));
            byte[] result;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
                binaryFormatter.Serialize(memoryStream, obj);
                result = memoryStream.ToArray();
            }
            return result;
        }

        public static T ToObject<T>(this byte[] bytes)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.All));
            T result;
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
                result = (T)((object)binaryFormatter.Deserialize(memoryStream));
            }
            return result;
        }

        public static T ToObject<T>(this byte[] bytes, SerializationBinder binder)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.All));
            T result;
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
                binaryFormatter.Binder = binder;
                result = (T)((object)binaryFormatter.Deserialize(memoryStream));
            }
            return result;
        }

        public static string ToCSV(this IEnumerable enumerable, string separator)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int num = 0;
            foreach (object current in enumerable)
            {
                stringBuilder.Append(current);
                stringBuilder.Append(separator);
                num++;
            }
            if (num > 0)
            {
                stringBuilder.Remove(stringBuilder.Length - separator.Length, separator.Length);
            }
            return stringBuilder.ToString();
        }

        public static string ToCSV<T>(this IEnumerable<T> enumerable, string separator, Func<T, string> formatter)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int num = 0;
            foreach (T current in enumerable)
            {
                stringBuilder.Append(formatter(current));
                stringBuilder.Append(separator);
                num++;
            }
            if (num > 0)
            {
                stringBuilder.Remove(stringBuilder.Length - separator.Length, separator.Length);
            }
            return stringBuilder.ToString();
        }

        public static T[] FromCSV<T>(this string csvValue, string separator) where T : IConvertible
        {
            List<T> list = new List<T>();
            int num = 0;
            int num2 = csvValue.IndexOf(separator, StringComparison.Ordinal);
            while (num2 >= 0 && num2 < csvValue.Length)
            {
                list.Add((T)((object)Convert.ChangeType(csvValue.Substring(num, num2 - num), typeof(T))));
                num = num2 + separator.Length;
                num2 = csvValue.IndexOf(separator, num, StringComparison.Ordinal);
            }
            if (!string.IsNullOrEmpty(csvValue))
            {
                list.Add((T)((object)Convert.ChangeType(csvValue.Substring(num, csvValue.Length - num), typeof(T))));
            }
            return list.ToArray();
        }

        public static T[] FromCSV<T>(this string csvValue, string separator, Func<string, T> converter)
        {
            List<T> list = new List<T>();
            int num = 0;
            int num2 = csvValue.IndexOf(separator, StringComparison.Ordinal);
            while (num2 >= 0 && num2 < csvValue.Length)
            {
                list.Add(converter(csvValue.Substring(num, num2 - num)));
                num = num2 + separator.Length;
                num2 = csvValue.IndexOf(separator, num, StringComparison.Ordinal);
            }
            if (!string.IsNullOrEmpty(csvValue))
            {
                list.Add(converter(csvValue.Substring(num, csvValue.Length - num)));
            }
            return list.ToArray();
        }
    }
}