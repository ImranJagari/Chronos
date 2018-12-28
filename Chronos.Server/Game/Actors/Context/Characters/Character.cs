using System;
using System.Linq;
using Chronos.Core.Extensions;
using Chronos.Core.Utils;
using Chronos.Protocol.Enums;
using Chronos.Protocol.Types;
using Chronos.Protocol.Types.ObjectsType;
using Chronos.Server.Databases.Characters;
using Chronos.Server.Game.Account;
using Chronos.Server.Game.Inventories;
using Chronos.Server.Game.Stats;
using Chronos.Server.Manager;
using Chronos.Server.Network;

namespace Chronos.Server.Game.Actors.Context.Characters
{
    public sealed class Character : ContextActor
    {
        public Character(CharacterRecord record)
        {
            Record = record;
            LoadRecord();
        }

        public CharacterRecord Record { get; }
        public SimpleClient Client { get; set; }

        public GameAccount Account => Client.Account;

        public override ObjectTypeEnum ObjectType => ObjectTypeEnum.OT_CLIENT;

        public override int Id
        {
            get => Record.Id;
            protected set => Record.Id = value;
        }

        public int AccountId
        {
            get => Record.AccountId;
            set => Record.AccountId = value;
        }

        public int Slot
        {
            get => Record.Slot;
            set => Record.Slot = value;
        }

        public override string Name
        {
            get => Record.Name;
            set => Record.Name = value;
        }

        public bool Sex
        {
            get => Record.Sex;
            set => Record.Sex = value;
        }

        public int Level
        {
            get => Record.Level;
            set => Record.Level = value;
        }

        public int Job
        {
            get => Record.Job;
            set => Record.Job = value;
        }

        public int HairMesh
        {
            get => Record.HairMesh;
            set => Record.HairMesh = value;
        }

        public uint HairColor
        {
            get => Record.HairColor;
            set => Record.HairColor = value;
        }

        public int HeadMesh
        {
            get => Record.HeadMesh;
            set => Record.HeadMesh = value;
        }

        public bool IsBlocked => BlockTime != null && BlockTime > DateTime.Now;

        public DateTime BlockTime
        {
            get => Record.BlockTime;
            set => Record.BlockTime = value;
        }

        public DateTime? DeletedDate
        {
            get => Record.DeletedDate;
            set => Record.DeletedDate = value;
        }

        public override StatsFields Stats { get; protected set; }
        public Inventory Inventory { get; private set; }

        public void LogOut()
        {
            try
            {
                SaveNow();
                Map.Leave(this);
            }
            catch (Exception e)
            {
                ConsoleUtils.WriteError(e.ToString());
            }
        }

        public void SaveNow()
        {
            Record.SceneId = Position.Map.SceneId;
            Record.X = Position.X;
            Record.Y = Position.Y;
            Record.Z = Position.Z;
            Record.Strenght = Stats.Fields[DefineEnum.STR].Base;
            Record.Stamina = Stats.Fields[DefineEnum.STA].Base;
            Record.Intelligence = Stats.Fields[DefineEnum.INT].Base;
            Record.EP = Stats.Fields[DefineEnum.EP].Base;
            Record.SPI = Stats.Fields[DefineEnum.SPI].Base;
            Record.Money = (uint) Stats.Fields[DefineEnum.MONEY].Base;
            Record.HP = Stats.Fields[DefineEnum.HP].Base;
            Record.DamageTaken = Stats.Fields[DefineEnum.HP].Base - Stats[DefineEnum.HP].Total;

            DatabaseManager.DefaultDatabase.Update(Record);
        }

        public void LoadRecord()
        {
            Stats = new StatsFields(this);
            Stats.Initialize(Record);

            Inventory = new Inventory();
            Inventory.LoadData(Id);
        }

        public override ObjectType GetObjectType(bool me = false)
        {
            return new CharacterObjectType(
                ObjectType,
                (uint) GetHashCode(),
                Sex ? (uint) 12 : 11,
                0xFFFFFFFF,
                Position.X,
                Position.Y,
                Position.Z,
                0,
                0,
                DEFAULT_SCALE,
                Name,
                Stats.Fields.Count,
                Stats.Fields.Keys.Select(x => (ushort) x).ToArray(),
                Stats.Fields.Values.Select(x => x.Total).ToArray(),
                0,
                new byte[0],
                new int[0],
                new int[0],
                (uint) Id,
                Sex ? (byte) 1 : (byte) 0,
                Job,
                (int) Account.Authority,
                SimpleServer.ServerId,
                "",
                (short) Record.Constellation,
                (short) Record.City_Code,
                (byte) HairMesh,
                HairColor,
                (byte) HeadMesh,
                0x7FFFFFFF /*Todo : option*/,
                0,
                "",
                0,
                0,
                0x64 /*Todo : fame*/,
                0,
                0,
                0,
                0x5322AFDF,
                0,
                0,
                new ItemType[0],
                0,
                new byte[0],
                new int[0],
                0,
                new int[0],
                8,
                0,
                new int[0],
                new int[0],
                new KingdomType(0,
                    0,
                    0,
                    "",
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    ""),
                new MasterType("",
                    "",
                    "",
                    0,
                    1,
                    0),
                new MarriageType(0),
                0 /*this.Inventory.ClosetItems.Count*/,
                new int[0]
                /*this.Inventory.ClosetItems.Select(x => x.Value.ClosetItemId).ToArray()*/,
                1,
                0,
                false,
                me,
                new FlagsDataType(0,
                    1,
                    DateTime.Now.GetUnixTimeStamp(),
                    0,
                    0,
                    new int[0],
                    2,
                    new[] {24000, 26000},
                    0,
                    new int[0],
                    new int[0],
                    new InventoryType((short) DefineEnum.MAX_INVENTORY + (short) DefineEnum.MAX_HUMAN_PARTS, (short)this.Inventory.Items.Count,
                        Inventory.InventorySlots,
                        (short)this.Inventory.Items.Count,
                        this.Inventory.Items.Values.Select(x => x.GetItemElementType()).ToArray(),
                        this.Inventory.Items.Values.Select(x => (short)x.Slot).ToArray()),
                    new QuestInventoryType(Inventory.QuestSlots,
                        0,
                        new ItemElementType[0],
                        new short[0]),
                    0,
                    new TaskbarType(
                        0,
                        new ShortcutType[1] {new ShortcutType(1, 1, ShortcutEnum.SKILL, 10101, 1, 10101, Id, 1, "")},
                        0,
                        new ShortcutType[1] {new ShortcutType(0, 0, ShortcutEnum.ITEM, 1, 1, 1, Id, 1, "")},
                        0,
                        new ShortcutType[0],
                        0,
                        new byte[] {1, 2, 3, 4},
                        0),
                    1,
                    new[] {new SkillType(10101, 2000)},
                    0,
                    false,
                    new FriendListType(FriendStateEnum.FRS_ONLINE,
                        0,
                        0,
                        new int[0],
                        0,
                        new FriendMemberType[0],
                        0,
                        new BlacklistedFriendMemberType[0],
                        0,
                        new FriendMemberType[0]),
                    new FactionType(0,
                        0,
                        0,
                        0,
                        0,
                        "",
                        new ProtegeType[0]),
                    0,
                    new MemorisedPositionType[0],
                    0,
                    0,
                    0,
                    0,
                    0,
                    135,
                    1,
                    new CreditCardType(CreditCardTypeEnum.CREDIT_CARD_NORMAL,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        (uint) DateTime.Now.GetUnixTimeStamp(),
                        0,
                        (uint) DateTime.Now.GetUnixTimeStamp(),
                        0,
                        -1,
                        0),
                    0,
                    0,
                    0,
                    0,
                    0,
                    StatsFields.StoneStrenghtBoost,
                    StatsFields.StoneDexterityBoost,
                    StatsFields.StoneStaminaBoost,
                    StatsFields.StoneSPIBoost,
                    StatsFields.StoneIntelligenceBoost,
                    0,
                    new LoverType[0],
                    8,
                    1,
                    Inventory.ClosetItems.Count,
                    Inventory.ClosetItems.Values.Select(x => x.GetFateClosetType()).ToArray(),
                    new VesselType(1,
                        0,
                        0,
                        0,
                        0,
                        new int[0],
                        new WingType[0]),
                    new HotkeyType(new HotkeyEnum[47],
                        new HotkeyEnum[47]),
                    new SpiritTatooType("1,10,2,0,1,10,3,0,1,10,2,0,1,10,1,0,1,10,4,0,1,10,4,0,"),
                    new AdventType(1, 0, 1, 0, "1=0;2=0;3=0;4=0;5=0;6=0;7=0;8=0;"),
                    new PetDomesticateType(0,
                        0,
                        new PetDomesticateStatsType[0]),
                    new ExpeditionType(1,
                        0,
                        0,
                        0,
                        "",
                        2004318103),
                    0,
                    0,
                    0));
        }

        public CharacterType GetNetwork() // Todo : Block time and IsBlocked + ClosetItems
        {
            return new CharacterType(Slot, Name, Id, Record.SceneId, Sex ? (byte) 1 : (byte) 0,
                new PositionType(Record.X, Record.Y, Record.Z), Level, Job, Stats[DefineEnum.STR].Total,
                Stats[DefineEnum.STA].Total, Stats[DefineEnum.DEX].Total,
                Stats[DefineEnum.INT].Total, Stats[DefineEnum.SPI].Total, HairMesh, HairColor, HeadMesh, 0, 0,
                Inventory.Items.Count,
                Inventory.Items.Values.Select(x => x.GetItemType()).ToArray(),
                Inventory.ClosetItems.Values.Select(x => x.GetClosetItemType()).ToArray());
        }
    }
}