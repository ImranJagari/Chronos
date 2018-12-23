using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chronos.Core.Collections
{
    public class LimitedOccurenceCounter<T> : IEnumerable<KeyValuePair<T, int>>
    {
        public LimitedOccurenceCounter(int limit)
        {
            Limit = limit;
            m_items = new Queue<T>(limit);
            m_occurenceDict = new Dictionary<T, int>();
        }

        private Queue<T> m_items;
        private Dictionary<T, int> m_occurenceDict; 

        public int Limit
        {
            get;
            set;
        }

        public IEnumerator<KeyValuePair<T, int>> GetEnumerator()
        {
            return m_occurenceDict.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            m_items.Enqueue(item);

            if (m_occurenceDict.ContainsKey(item))
                m_occurenceDict[item]++;
            else
                m_occurenceDict.Add(item, 1);

            if (m_items.Count > Limit)
                Dequeue();
        }

        public void Add(T item, int occurences)
        {
            for (int i = 0; i < occurences; i++)
            {
                m_items.Enqueue(item);
            }

            if (m_occurenceDict.ContainsKey(item))
                m_occurenceDict[item] += occurences;
            else
                m_occurenceDict.Add(item, occurences);

            while (m_items.Count > Limit)
                Dequeue();
        }

        public void Clear()
        {
            m_items.Clear();
            m_occurenceDict.Clear();
        }

        public bool Contains(T item)
        {
            return m_items.Contains(item);
        }

        public T Dequeue()
        {
            var item = m_items.Dequeue();
            if (item != null)
            {
                if (m_occurenceDict[item]-- == 0)
                    m_occurenceDict.Remove(item);

                return item;
            }

            return default(T);
        }

        public int GetOccurence(T item)
        {
            return m_occurenceDict.TryGetValue(item, out var result) ? result : 0;
        }

        public int GetPercentile(double percentile)
        {
            if (m_occurenceDict.Count == 0)
                return 0;

            return m_occurenceDict.Values.OrderBy(x => x).ElementAt((int)((m_occurenceDict.Count-1)*percentile));
        }

        public int GetMedian()
        {
            return GetPercentile(0.5);
        }

        public int Count => m_items.Count;

        public bool IsReadOnly => false;
    }
}