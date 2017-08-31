using Chronos.Server.Databases.Characters;
using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Game.Stats
{
    public delegate short StatsFormulasHandler(IStatsOwner target);

    public class StatsFields
    {
        public Dictionary<DefineEnum, StatsData> Fields
        {
            get;
            private set;
        }
        public StatsData this[DefineEnum name]
        {
            get
            {
                StatsData value;
                return Fields.TryGetValue(name, out value) ? value : null;
            }
        }
        public StatsFields(IStatsOwner owner)
        {
            Owner = owner;
            Fields = new Dictionary<DefineEnum, StatsData>();
        }

        public IStatsOwner Owner
        {
            get;
            private set;
        }
        public void Initialize(CharacterRecord record)
        {
            // note : keep this order !
            Fields = new Dictionary<DefineEnum, StatsData>();

            Fields.Add(DefineEnum.STR, new StatsData(Owner, DefineEnum.STR, record.Strenght));
            Fields.Add(DefineEnum.STA, new StatsData(Owner, DefineEnum.STA, record.Stamina));
            Fields.Add(DefineEnum.DEX, new StatsData(Owner, DefineEnum.DEX, record.Dexterity));
            Fields.Add(DefineEnum.INT, new StatsData(Owner, DefineEnum.INT, record.Intelligence));
            Fields.Add(DefineEnum.SPI, new StatsData(Owner, DefineEnum.SPI, record.SPI));
            Fields.Add(DefineEnum.HP, new StatsData(Owner, DefineEnum.HP, record.HP));
            Fields.Add(DefineEnum.MONEY, new StatsData(Owner, DefineEnum.MONEY, (int)record.Money));
            Fields.Add(DefineEnum.GOLD, new StatsData(Owner, DefineEnum.GOLD, 0));
            Fields.Add(DefineEnum.EP, new StatsData(Owner, DefineEnum.EP, record.EP));
        }
    }
}
