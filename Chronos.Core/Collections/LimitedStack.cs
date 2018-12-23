using System;
using System.Collections.Generic;

namespace Chronos.Core.Collections
{
    [Serializable]
    public class LimitedStack<T> : LinkedList<T>
    {
        private int m_maxItems;

        public LimitedStack(int num)
        {
            m_maxItems = num;
        }

        public LimitedStack(int num, IEnumerable<T> items)
            : base(items)
        {
            m_maxItems = num;
        }

        public int MaxItems
        {
            get { return m_maxItems; }
            set
            {
                while (Count > value)
                {
                    RemoveFirst();
                }
                m_maxItems = value;
            }
        }

        public T Peek()
        {
            return Last.Value;
        }

        public T Pop()
        {
            var node = Last;
            RemoveLast();
            return node.Value;
        }

        public void Push(T value)
        {
            var node = new LinkedListNode<T>(value);
            AddLast(node);

            if (Count > m_maxItems)
            {
                RemoveFirst();
            }
        }
    }
}