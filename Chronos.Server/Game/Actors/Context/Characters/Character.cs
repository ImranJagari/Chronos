using Chronos.Server.Databases.Characters;
using Chronos.Server.Game.Inventories;
using Chronos.Server.Game.Stats;
using Chronos.Server.Network;
using Chronos.Protocol.Enums;
using Chronos.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Server.Game.Account;
using Chronos.Server.Game.World;
using Chronos.Protocol.Types.ObjectsType;
using Chronos.Core.Extensions;
using Chronos.Server.Manager.Maps;
using Chronos.Core.Utils;
using Chronos.Server.Manager;

namespace Chronos.Server.Game.Actors.Context.Characters
{
    public class Character : ContextActor
    {
        public CharacterRecord Record { get; }

        public Character(CharacterRecord record)
        {
            Record = record;
            LoadRecord();
        }
        public SimpleClient Client { get; set; }

        public GameAccount Account => Client.Account;

        public override ObjectTypeEnum ObjectType => ObjectTypeEnum.OT_CLIENT;

        public override int Id
        {
            get { return Record.Id; }
            protected set { Record.Id = value; }
        }
        public int AccountId
        {
            get { return Record.AccountId; }
            set { Record.AccountId = value; }
        }
        public override string Name
        {
            get { return Record.Name; }
            set { Record.Name = value; }
        }
        
        public bool Sex
        {
            get { return Record.Sex; }
            set { Record.Sex = value; }
        }
        public int Level
        {
            get { return Record.Level; }
            set { Record.Level = value; }
        }
        public int Job
        {
            get { return Record.Job; }
            set { Record.Job = value; }
        }

        public int HairMesh
        {
            get { return Record.HairMesh; }
            set { Record.HairMesh = value; }
        }
        public uint HairColor
        {
            get { return Record.HairColor; }
            set { Record.HairColor = value; }
        }
        public int HeadMesh
        {
            get { return Record.HeadMesh; }
            set { Record.HeadMesh = value; }
        }

        public bool IsBlocked => BlockTime != null && BlockTime > DateTime.Now;
        public DateTime BlockTime
        {
            get { return Record.BlockTime; }
            set { Record.BlockTime = value; }
        }
        public DateTime? DeletedDate
        {
            get { return Record.DeletedDate; }
            set { Record.DeletedDate = value; }
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
            catch(Exception e)
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
            Record.Money = (uint)Stats.Fields[DefineEnum.MONEY].Base;
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
            return new CharacterObjectType(ObjectType, (uint)this.GetHashCode(), this.Sex ? (uint)12 : 11, (uint)0xFFFFFFFF,
                this.Position.X, this.Position.Y, this.Position.Z, 0, 0, DEFAULT_SCALE, this.Name,
                this.Stats.Fields.Count, this.Stats.Fields.Keys.Select(x => (ushort)x).ToArray(),
                this.Stats.Fields.Values.Select(x => x.Total).ToArray(), 0, new byte[0], new int[0], new int[0],
                (uint)this.Id, this.Sex ? (byte)1 : (byte)0, this.Job, (int)Account.Authority,
                SimpleServer.ServerId, "", (short)this.Record.Constellation, (short)this.Record.City_Code, (byte)this.HairMesh,
                this.HairColor, (byte)this.HeadMesh, (uint)0x7FFFFFFF /*Todo : option*/, 0, "", 0, 0, 0x64/*Todo : fame*/,
                0, 0, 0, 0x5322AFDF, 0, 0, new ItemType[0], 0, new byte[0], new int[0], 0, new int[0], 7, 0, new int[0], new int[0],
                new KingdomType((byte)0, (byte)0, 0, $"", (byte)0, 0, 0, 0, 0, 0, 0, $""),
                new MasterType("", "", "", 0, 0, 0), new MarriageType(0), this.Inventory.ClosetItems.Count,
                this.Inventory.ClosetItems.Select(x => x.Value.ClosetItemId).ToArray(), 0, 0, false, me,
                new FlagsDataType(0, 1, DateTime.Now.GetUnixTimeStamp(), 0, 0, new int[0], 0, new int[0], 0, new int[0], new int[0],
                new InventoryType(Inventory.InventorySlots, 0, new ItemElementType[0], new short[0]),
                new QuestInventoryType(Inventory.QuestSlots, 0, new ItemElementType[0], new short[0]), 0,
                new TaskbarType(0, new ShortcutType[0], 0, new ShortcutType[0], 0,
                new ShortcutType[0], 4, new Byte[] { 1, 2, 3, 4 }, 0), 0, new SkillType[0],
                0, false,
                new FriendListType(FriendStateEnum.FRS_ONLINE, 0, 0, new int[0], 0, new FriendMemberType[0], 0, new FriendMemberType[0], 0, new FriendMemberType[0]),
                new FactionType(0, 0, 0, 0, 0, "", new ProtegeType[0]), 0,
                new MemorisedPositionType[0], 0, 0, 0, 0, 0, 135, 1,
                new CreditCardType(CreditCardTypeEnum.CREDIT_CRAD_NORMAL, 0, 0, 0, 0, 0, 0, 0, (uint)DateTime.Now.GetUnixTimeStamp(), 0, (uint)DateTime.Now.GetUnixTimeStamp(), 0, -1, 0),
                0, 0, 0, 0, 0, StatsFields.StoneStrenghtBoost, StatsFields.StoneDexterityBoost, StatsFields.StoneStaminaBoost, StatsFields.StoneSPIBoost,
                StatsFields.StoneIntelligenceBoost, 0, new LoverType[0], 8, 1, this.Inventory.ClosetItems.Count,
                this.Inventory.ClosetItems.Values.Select(x => x.GetFateClosetType()).ToArray(),
                new VesselType(1, 0, 0, 0, 0, new int[0], new WingType[0]), new HotkeyType(new HotkeyEnum[47], new HotkeyEnum[47]),
                new SpiritTatooType("1,10,2,0,1,10,3,0,1,10,2,0,1,10,1,0,1,10,4,0,1,10,4,0,"), new AdventType(1, 0, 1, 0, "1=0;2=0;3=0;4=0;5=0;6=0;7=0;8=0;"),
                new PetDomesticateType(0, 0, new PetDomesticateStatsType[0]), new ExpeditionType(1, 0, 0, 0, "", 2004318103), 0, 0, 0));
        }
        public CharacterType GetNetwork()// Todo : Block time and IsBlocked + ClosetItems
        {
            return new CharacterType(this.Client.Account.Characters.Where(x => !x.DeletedDate.HasValue).ToList().IndexOf(this), Name, Id, Record.SceneId, Sex ? (byte)1 : (byte)0,
                new PositionType(Record.X, Record.Y, Record.Z), Level, Job, Stats[DefineEnum.STR].Total, Stats[DefineEnum.STA].Total, Stats[DefineEnum.DEX].Total,
                Stats[DefineEnum.INT].Total, Stats[DefineEnum.SPI].Total, HairMesh, HairColor, HeadMesh, 0, 0, Inventory.Items.Count,
                Inventory.Items.Values.Select(x => x.GetNetwork()).ToArray(), Inventory.ClosetItems.Values.Select(x => x.GetNetwork()).ToArray());
        }
    }
}
