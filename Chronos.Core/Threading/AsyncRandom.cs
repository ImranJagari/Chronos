using System;
using System.Threading;

namespace Chronos.Core.Threading
{
    /// <summary>
    ///   Represent a Random class that generate a thread unique seed
    /// </summary>
    public sealed class AsyncRandom : Random
    {
        private static int m_incrementer;

        public AsyncRandom()
            : base (Environment.TickCount + Thread.CurrentThread.ManagedThreadId + m_incrementer)
        {
            unchecked
            {
                Interlocked.Increment(ref m_incrementer);
            }
        }

        public AsyncRandom(int seed)
            : base(seed)
        {
            
        }

        public double NextDouble(double min, double max)
        {
            return NextDouble() * ( max - min ) + min;
        }
    }
}