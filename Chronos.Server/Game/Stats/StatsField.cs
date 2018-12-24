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
        public static byte StoneIntelligenceBoost = 0;
        [Variable]
        public static byte StoneStrenghtBoost = 0;
        [Variable]
        public static byte StoneStaminaBoost = 0;
        [Variable]
        public static byte StoneSPIBoost = 0;
        [Variable]
        public static byte StoneDexterityBoost = 0;



        public Dictionary<DefineEnum, StatsData> Fields
        {
            get;
            private set;
        }
        public StatsData this[DefineEnum name]
        {
            get
            {
                return Fields.TryGetValue(name, out var value) ? value : null;
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
            Fields = new Dictionary<DefineEnum, StatsData>
            {
                {DefineEnum.HP, new StatsData(Owner, DefineEnum.HP, record.HP)},
                {DefineEnum.MP, new StatsData(Owner, DefineEnum.MP, 200)},
                {DefineEnum.GP, new StatsData(Owner, DefineEnum.GP, 2)},
                {DefineEnum.LV, new StatsData(Owner, DefineEnum.LV, record.Level)},
                {DefineEnum.VIT, new StatsData(Owner, DefineEnum.VIT, 290)},
                {DefineEnum.FLV, new StatsData(Owner, DefineEnum.FLV, 1)},
                {DefineEnum.FHP, new StatsData(Owner, DefineEnum.FHP, 450)},
                {DefineEnum.FMP, new StatsData(Owner, DefineEnum.FMP, 290)},
                {DefineEnum.MOVE_SPEED, new StatsData(Owner, DefineEnum.MOVE_SPEED, 6000)},
                {DefineEnum.STR, new StatsData(Owner, DefineEnum.STR, record.Strenght)},
                {DefineEnum.STA, new StatsData(Owner, DefineEnum.STA, record.Stamina)},
                {DefineEnum.INT, new StatsData(Owner, DefineEnum.INT, record.Intelligence)},
                {DefineEnum.DEX, new StatsData(Owner, DefineEnum.DEX, record.Dexterity)},
                {DefineEnum.SPI, new StatsData(Owner, DefineEnum.SPI, record.SPI)},
                {DefineEnum.MONEY, new StatsData(Owner, DefineEnum.MONEY, (int) record.Money)},
                {DefineEnum.GOLD, new StatsData(Owner, DefineEnum.GOLD, (int) 0)},
                {DefineEnum.EP, new StatsData(Owner, DefineEnum.EP, (int)record.Experience << 31 >> 31)},
                {DefineEnum.EP2, new StatsData(Owner, DefineEnum.EP, 1500000 << 31 >> 31)},
                {DefineEnum.MAXHP, new StatsData(Owner, DefineEnum.MAXHP, record.HP)},
                {DefineEnum.MAXMP, new StatsData(Owner, DefineEnum.MAXHP, 200)},
                {DefineEnum.ATK_BASE, new StatsData(Owner, DefineEnum.ATK_BASE, 10)}
            };
        }

        public static Dictionary<DefineEnum, StatsData> LoadInertieData()
        {
            var fields = new Dictionary<DefineEnum, StatsData>
            {
                {DefineEnum.HP, new StatsData(null, DefineEnum.HP, 100)},
                {DefineEnum.MP, new StatsData(null, DefineEnum.MP, 200)},
                {DefineEnum.GP, new StatsData(null, DefineEnum.GP, 2)},
                {DefineEnum.LV, new StatsData(null, DefineEnum.LV, 1)},
                {DefineEnum.VIT, new StatsData(null, DefineEnum.VIT, 290)},
                {DefineEnum.FLV, new StatsData(null, DefineEnum.FLV, 1)},
                {DefineEnum.FHP, new StatsData(null, DefineEnum.FHP, 450)},
                {DefineEnum.FMP, new StatsData(null, DefineEnum.FMP, 290)},
                {DefineEnum.MOVE_SPEED, new StatsData(null, DefineEnum.MOVE_SPEED, 7000)},
                {DefineEnum.STR, new StatsData(null, DefineEnum.STR, 5)},
                {DefineEnum.STA, new StatsData(null, DefineEnum.STA, 5)},
                {DefineEnum.INT, new StatsData(null, DefineEnum.INT, 5)},
                {DefineEnum.DEX, new StatsData(null, DefineEnum.DEX, 5)},
                {DefineEnum.SPI, new StatsData(null, DefineEnum.SPI, 5)},
                {DefineEnum.MONEY, new StatsData(null, DefineEnum.MONEY, (int) 10)},
                {DefineEnum.GOLD, new StatsData(null, DefineEnum.GOLD, (int) 0)},
                {DefineEnum.EP, new StatsData(null, DefineEnum.EP, 5000 << 31 >> 31)},
                {DefineEnum.EP2, new StatsData(null, DefineEnum.EP2, 5000 >> 31)},
                {DefineEnum.MAXHP, new StatsData(null, DefineEnum.MAXHP, 100)},
                {DefineEnum.MAXMP, new StatsData(null, DefineEnum.MAXHP, 200)}
            };

            return fields;
        }
    }
}
