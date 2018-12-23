using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Core.Timers
{
    public class TimedTimerEntry : IDisposable
    {
        private int m_interval;
        private bool m_firstCalled;

        public TimedTimerEntry()
        {

        }

        public TimedTimerEntry(int interval, Action action)
            : this(interval, interval, action)
        {
        }

        public TimedTimerEntry(int delay, int interval, Action action)
        {
            m_delay = delay;
            Interval = interval;
            Action = action;
        }

        public bool Enabled
        {
            get;
            private set;
        }

        public bool IsDisposed
        {
            get;
            private set;
        }

        public Action Action
        {
            get;
            set;
        }

        public DateTime NextTick
        {
            get;
            private set;
        }

        public DateTime? LastExecute
        {
            get;
            private set;
        }

        private int m_delay;

        public int Delay
        {
            get { return m_delay; }
            set
            {
                if (!m_firstCalled && Enabled && value != -1)
                {
                    NextTick = NextTick - TimeSpan.FromMilliseconds(m_delay) + TimeSpan.FromMilliseconds(value);
                }

                m_delay = value;
            }
        }

        public int Interval
        {
            get { return m_interval; }
            set
            {
                if (value != -1)
                    NextTick = NextTick - TimeSpan.FromMilliseconds(m_interval) + TimeSpan.FromMilliseconds(value);
                m_interval = value;
            }
        }

        public void Start()
        {
            NextTick = DateTime.Now + TimeSpan.FromMilliseconds(m_delay);
            Enabled = true;
        }

        public void Stop()
        {
            Enabled = false;
        }

        public void Dispose()
        {
            Enabled = false;
            IsDisposed = true;
        }


        public bool ShouldTrigger()
        {
            return Enabled && !IsDisposed && DateTime.Now >= NextTick;
        }

        public bool Trigger()
        {
            Action();

            if (Interval < 0)
                Enabled = false;
            else
                NextTick = DateTime.Now + TimeSpan.FromMilliseconds(Interval);

            m_firstCalled = true;
            LastExecute = DateTime.Now;

            return Enabled || IsDisposed;
        }

        public override string ToString()
        {
            return string.Format("{0} (Callback = {1}.{2}, Delay = {3})", GetType(), Action.Target, Action.Method, Delay);
        }
    }

    public class TimedTimerComparer : IComparer<TimedTimerEntry>
    {
        public int Compare(TimedTimerEntry x, TimedTimerEntry y)
        {
            return x.NextTick.CompareTo(y.NextTick);
        }
    }
}
