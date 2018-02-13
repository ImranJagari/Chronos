using Chronos.Server.Databases.Characters;
using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Core.Attributes;

namespace Chronos.Server.Game.Stats
{
    public delegate short StatsFormulasHandler(IStatsOwner target);

    public class StatsFields
    {
        [Variable]
        public static byte StoneIntelligenceBoost = 15;
        [Variable]
        public static byte StoneStrenghtBoost = 15;
        [Variable]
        public static byte StoneStaminaBoost = 15;
        [Variable]
        public static byte StoneSPIBoost = 15;
        [Variable]
        public static byte StoneDexterityBoost = 15;



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
            Fields = new Dictionary<DefineEnum, StatsData>();

            Fields.Add(DefineEnum.HP, new StatsData(Owner, DefineEnum.HP, record.HP));
            Fields.Add(DefineEnum.MP, new StatsData(Owner, DefineEnum.MP, 200));
            Fields.Add(DefineEnum.GP, new StatsData(Owner, DefineEnum.GP, 2));
            Fields.Add(DefineEnum.LV, new StatsData(Owner, DefineEnum.LV, record.Level));
            Fields.Add(DefineEnum.VIT, new StatsData(Owner, DefineEnum.VIT, 290));
            Fields.Add(DefineEnum.FLV, new StatsData(Owner, DefineEnum.FLV, 1));
            Fields.Add(DefineEnum.FHP, new StatsData(Owner, DefineEnum.FHP, 450));
            Fields.Add(DefineEnum.FMP, new StatsData(Owner, DefineEnum.FMP, 290));

            Fields.Add(DefineEnum.MOVE_SPEED, new StatsData(Owner, DefineEnum.MOVE_SPEED, 6000));

            Fields.Add(DefineEnum.STR, new StatsData(Owner, DefineEnum.STR, record.Strenght));
            Fields.Add(DefineEnum.STA, new StatsData(Owner, DefineEnum.STA, record.Stamina));
            Fields.Add(DefineEnum.INT, new StatsData(Owner, DefineEnum.INT, record.Intelligence));
            Fields.Add(DefineEnum.DEX, new StatsData(Owner, DefineEnum.DEX, record.Dexterity));
            Fields.Add(DefineEnum.SPI, new StatsData(Owner, DefineEnum.SPI, record.SPI));

            Fields.Add(DefineEnum.MONEY, new StatsData(Owner, DefineEnum.MONEY, (int)record.Money));
            Fields.Add(DefineEnum.GOLD, new StatsData(Owner, DefineEnum.GOLD, (int)3000));

            Fields.Add(DefineEnum.EP, new StatsData(Owner, DefineEnum.EP, record.EP));
            Fields.Add(DefineEnum.EP2, new StatsData(Owner, DefineEnum.EP2, 5000));

            Fields.Add(DefineEnum.MAXHP, new StatsData(Owner, DefineEnum.MAXHP, record.HP));
            Fields.Add(DefineEnum.MAXMP, new StatsData(Owner, DefineEnum.MAXHP, 200));

        }
    }
}
