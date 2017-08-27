using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Game.Stats
{
    public class StatsData
    {
        protected StatsFormulasHandler m_formulas;
        protected int ValueBase;
        protected int ValueContext;
        protected int ValueEquiped;
        protected int ValueGiven;
        protected int ValueAdditionnal;
        int? m_limit;
        readonly bool m_limitEquippedOnly;

        public StatsData(IStatsOwner owner, DefineEnum name, int valueBase, StatsFormulasHandler formulas = null)
        {
            ValueBase = valueBase;
            m_formulas = formulas;
            Name = name;
            Owner = owner;
        }
        public StatsData(IStatsOwner owner, DefineEnum name, int valueBase, int? limit, bool limitEquippedOnly = false)
        {
            ValueBase = valueBase;
            m_limit = limit;
            m_limitEquippedOnly = limitEquippedOnly;
            Name = name;
            Owner = owner;
        }

        public IStatsOwner Owner
        {
            get;
            protected set;
        }

        public DefineEnum Name
        {
            get;
            protected set;
        }

        public virtual int Base
        {
            get
            {
                return m_formulas != null ? m_formulas(Owner) + ValueBase : ValueBase;
            }
            set
            {
                ValueBase = value;
                OnModified();
            }
        }

        public virtual int Equiped
        {
            get { return ValueEquiped; }
            set
            {
                ValueEquiped = value;
                OnModified();
            }
        }

        public virtual int Given
        {
            get { return ValueGiven; }
            set
            {
                ValueGiven = value;
                OnModified();
            }
        }

        public virtual int Context
        {
            get { return ValueContext; }
            set
            {
                ValueContext = value;
                OnModified();
            }
        }

        public virtual int Additional
        {
            get { return ValueAdditionnal; }
            set
            {
                ValueAdditionnal = value;
                OnModified();
            }
        }

        public virtual int Total
        {
            get
            {
                var totalNoBoost = Base + Additional + Equiped;

                if (m_limitEquippedOnly && Limit != null && totalNoBoost > Limit.Value)
                    totalNoBoost = Limit.Value;

                var total = totalNoBoost + Given + Context;

                if (Limit != null && !m_limitEquippedOnly && total > Limit.Value)
                    total = Limit.Value;

                return total;
            }
        }

        /// <summary>
        ///   TotalSafe can't be lesser than 0
        /// </summary>
        public virtual int TotalSafe
        {
            get
            {
                var total = Total;

                return total > 0 ? total : 0;
            }
        }

        public virtual int TotalWithoutContext => Total - Context;

        public virtual int? Limit
        {
            get { return m_limit; }
            set
            {
                m_limit = value;
                OnModified();
            }
        }

        public event Action<StatsData, int> Modified;

        protected virtual void OnModified()
        {
            var handler = Modified;
            if (handler != null) handler(this, Total);
        }

        public static int operator +(int i1, StatsData s1)
        {
            return i1 + s1.Total;
        }

        public static int operator +(StatsData s1, StatsData s2)
        {
            return s1.Total + s2.Total;
        }

        public static int operator -(int i1, StatsData s1)
        {
            return i1 - s1.Total;
        }

        public static int operator -(StatsData s1, StatsData s2)
        {
            return s1.Total - s2.Total;
        }

        public static int operator *(int i1, StatsData s1)
        {
            return i1 * s1.Total;
        }

        public static int operator *(StatsData s1, StatsData s2)
        {
            return s1.Total * s2.Total;
        }

        public static double operator /(StatsData s1, double d1)
        {
            return s1.Total / d1;
        }

        public static double operator /(StatsData s1, StatsData s2)
        {
            return s1.Total / (double)s2.Total;
        }



        public override string ToString()
        {
            return string.Format("{0}({1}+{2}+{3}+{4})", Total, Base, Additional, Equiped, Context);
        }

        public virtual StatsData CloneAndChangeOwner(IStatsOwner owner)
        {
            var clone = new StatsData(owner, Name, Base, Limit, m_limitEquippedOnly)
            {
                Additional = Additional,
                Context = Context,
                Equiped = Equiped,
                Given = Given
            };

            return clone;
        }
    }
}
