using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronos.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static List<T> Populate<T>(this List<T> list, T value, int maxNumber)
        {
            while (list.Count < maxNumber)
            {
                list.Add(value);
            }
            return list;
        }
        public static string ByteArrayToString(this byte[] bytes)
        {
            var output = new StringBuilder(bytes.Length);

            for (int i = 0; i < bytes.Length; i++)
            {
                output.Append(bytes[i].ToString("X2"));
            }

            return output.ToString().ToLower();
        }

        public static string EncodeByteArray(this byte[] bytes)
        {
            var output = new StringBuilder(bytes.Length);

            foreach (byte t in bytes)
            {
                output.Append((char)t);
            }

            return output.ToString();
        }

        public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static bool CompareEnumerable<T>(this IEnumerable<T> ie1, IEnumerable<T> ie2)
        {
            if (ie1.GetType() != ie2.GetType())
                return false;

            var a1 = ie1.ToArray();
            var a2 = ie2.ToArray();

            if (a1.Length != a2.Length)
                return false;

            return a1.Except(a2).Count() == 0 && a2.Except(a1).Count() == 0;
        }

        public static T MaxOf<T, T1>(this IList<T> collection, Func<T, T1> selector) where T1 : IComparable<T1>
        {
            if (collection.Count == 0) return default(T);

            T maxT = collection[0];
            T1 maxT1 = selector(maxT);

            for (int i = 1; i < collection.Count; i++)
            {
                T1 currentT1 = selector(collection[i]);
                if (currentT1.CompareTo(maxT1) > 0)
                {
                    maxT = collection[i];
                    maxT1 = currentT1;
                }
            }
            return maxT;
        }

        public static T MinOf<T, T1>(this IList<T> collection, Func<T, T1> selector) where T1 : IComparable<T1>
        {
            if (collection.Count == 0) return default(T);

            T maxT = collection[0];
            T1 maxT1 = selector(maxT);

            for (int i = 1; i < collection.Count; i++)
            {
                T1 currentT1 = selector(collection[i]);
                if (currentT1.CompareTo(maxT1) < 0)
                {
                    maxT = collection[i];
                    maxT1 = currentT1;
                }
            }
            return maxT;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            var rand = new Random();

            T[] elements = enumerable.ToArray();
            // Note i > 0 to avoid final pointless iteration
            for (int i = elements.Length - 1; i > 0; i--)
            {
                // Swap element "i" with a random earlier element it (or itself)
                int swapIndex = rand.Next(i + 1);
                T tmp = elements[i];
                elements[i] = elements[swapIndex];
                elements[swapIndex] = tmp;
            }
            // Lazily yield (avoiding aliasing issues etc)
            foreach (T element in elements)
            {
                yield return element;
            }
        }

        public static IEnumerable<T> ShuffleWithProbabilities<T>(this IEnumerable<T> enumerable,
                                                                 IEnumerable<int> probabilities)
        {
            var rand = new Random();

            var elements = enumerable.ToList();
            var result = new T[elements.Count];
            var indices = probabilities.ToList();

            if (elements.Count != indices.Count)
                throw new Exception("Probabilities must have the same length that the enumerable");

            int sum = indices.Sum();

            if (sum == 0)
                return Shuffle(elements);

            for (int i = 0; i < result.Length; i++)
            {
                int randInt = rand.Next(sum + 1);
                int currentSum = 0;
                for (int j = 0; j < indices.Count; j++)
                {
                    currentSum += indices[j];

                    if (currentSum >= randInt)
                    {
                        result[i] = elements[j];

                        sum -= indices[j];
                        indices.RemoveAt(j);
                        elements.RemoveAt(j);
                        break;
                    }
                }
            }

            return result;
        }

        public static T RandomElementOrDefault<T>(this IEnumerable<T> enumerable)
        {
            var rand = new Random();
            int count = enumerable.Count();

            if (count <= 0)
                return default(T);

            return enumerable.ElementAt(rand.Next(count));
        }
        /// <summary>
        /// Get index of an element in an array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] array, T element)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if(array[i].Equals(element))
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Returns the string representation of an IEnumerable (all elements, joined by comma)
        /// </summary>
        /// <param name="conj">The conjunction to be used between each elements of the collection</param>
        public static string ToStringCol(this ICollection collection, string conj)
        {
            return collection != null ? string.Join(conj, ToStringArr(collection)) : "(null)";
        }

        public static string ToString(this IEnumerable collection, string conj)
        {
            return collection != null ? string.Join(conj, ToStringArr(collection)) : "(null)";
        }

        public static string[] ToStringArr(this IEnumerable collection)
        {
            var strs = new List<string>();
            IEnumerator colEnum = collection.GetEnumerator();
            while (colEnum.MoveNext())
            {
                object cur = colEnum.Current;
                if (cur != null)
                {
                    strs.Add(cur.ToString());
                }
            }
            return strs.ToArray();
        }

        public static T GetOrDefault<T>(this IList<T> list, int index)
        {
            return index >= list.Count ? default(T) : list[index];
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            TValue val;
            return dict.TryGetValue(key, out val) ? val : default(TValue);
        }
    }
}