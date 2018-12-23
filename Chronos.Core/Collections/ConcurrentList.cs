using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Chronos.Core.Collections
{
    public class ConcurrentList<T> : IList<T>, IList
    {
        private readonly List<T> m_underlyingList = new List<T>();
        private readonly object m_syncRoot = new object();
        private readonly ConcurrentQueue<T> m_underlyingQueue;
        private bool m_requiresSync;
        private bool m_isDirty;

        public ConcurrentList()
        {
            m_underlyingQueue = new ConcurrentQueue<T>();
        }

        public ConcurrentList(IEnumerable<T> items)
        {
            m_underlyingQueue = new ConcurrentQueue<T>(items);
            m_isDirty = true;
        }

        private void UpdateLists()
        {
            if (!m_isDirty)
                return;

            lock (m_syncRoot)
            {
                m_requiresSync = true;
                while (m_underlyingQueue.TryDequeue(out T temp))
                    m_underlyingList.Add(temp);
                m_requiresSync = false;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (m_syncRoot)
            {
                UpdateLists();
                return m_underlyingList.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (m_requiresSync)
                lock (m_syncRoot)
                    m_underlyingQueue.Enqueue(item);
            else
                m_underlyingQueue.Enqueue(item);
            m_isDirty = true;
        }

        public int Add(object value)
        {
            if (m_requiresSync)
                lock (m_syncRoot)
                    m_underlyingQueue.Enqueue((T)value);
            else
                m_underlyingQueue.Enqueue((T)value);
            m_isDirty = true;
            lock (m_syncRoot)
            {
                UpdateLists();
                return m_underlyingList.IndexOf((T)value);
            }
        }

        public void AddRange(IEnumerable<T> items)
        {
            if (m_requiresSync)
                lock (m_syncRoot)
                    foreach(var item in items)
                        m_underlyingQueue.Enqueue(item);
            else
                foreach (var item in items)
                    m_underlyingQueue.Enqueue(item);

            m_isDirty = true;
        }

        public bool Contains(object value)
        {
            lock (m_syncRoot)
            {
                UpdateLists();
                return m_underlyingList.Contains((T)value);
            }
        }

        public int IndexOf(object value)
        {
            lock (m_syncRoot)
            {
                UpdateLists();
                return m_underlyingList.IndexOf((T)value);
            }
        }

        public void Insert(int index, object value)
        {
            lock (m_syncRoot)
            {
                UpdateLists();
                m_underlyingList.Insert(index, (T)value);
            }
        }

        public void Remove(object value)
        {
            lock (m_syncRoot)
            {
                UpdateLists();
                m_underlyingList.Remove((T)value);
            }
        }

        public void RemoveAt(int index)
        {
            lock (m_syncRoot)
            {
                UpdateLists();
                m_underlyingList.RemoveAt(index);
            }
        }



        public T this[int index]
        {
            get
            {
                lock (m_syncRoot)
                {
                    UpdateLists();
                    return m_underlyingList[index];
                }
            }
            set
            {
                lock (m_syncRoot)
                {
                    UpdateLists();
                    m_underlyingList[index] = value;
                }
            }
        }

        object IList.this[int index]
        {
            get { return ((IList<T>)this)[index]; }
            set { ((IList<T>)this)[index] = (T)value; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public void Clear()
        {
            lock (m_syncRoot)
            {
                UpdateLists();
                m_underlyingList.Clear();
            }
        }

        public bool Contains(T item)
        {
            lock (m_syncRoot)
            {
                UpdateLists();
                return m_underlyingList.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (m_syncRoot)
            {
                UpdateLists();
                m_underlyingList.CopyTo(array, arrayIndex);
            }
        }

        public bool Remove(T item)
        {
            lock (m_syncRoot)
            {
                UpdateLists();
                return m_underlyingList.Remove(item);
            }
        }

        public void CopyTo(Array array, int index)
        {
            lock (m_syncRoot)
            {
                UpdateLists();
                m_underlyingList.CopyTo((T[])array, index);
            }
        }

        public int Count
        {
            get
            {
                lock (m_syncRoot)
                {
                    UpdateLists();
                    return m_underlyingList.Count;
                }
            }
        }

        public object SyncRoot
        {
            get { return m_syncRoot; }
        }

        public bool IsSynchronized
        {
            get { return true; }
        }

        public int IndexOf(T item)
        {
            lock (m_syncRoot)
            {
                UpdateLists();
                return m_underlyingList.IndexOf(item);
            }
        }

        public void Insert(int index, T item)
        {
            lock (m_syncRoot)
            {
                UpdateLists();
                m_underlyingList.Insert(index, item);
            }
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            return new ReadOnlyCollection<T>(m_underlyingList);
        }
    }
}
