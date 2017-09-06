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

        public override ObjectTypeEnum ObjectType => ObjectTypeEnum.OT_PLAYER;

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
        public string Name
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

            SimpleServer.Instance.DBAccessor.Database.Update(Record);
        }
        public void LoadRecord()
        {
            Stats = new StatsFields(this);
            Stats.Initialize(Record);

            Inventory = new Inventory();
            Inventory.LoadData(Id);

            Position = new ObjectPosition(new Map() { SceneId = Record.SceneId }, this.Record.X, Record.Y, Record.Z);
        }
        public CharacterType GetNetwork()// Todo : Block time and IsBlocked + ClosetItems
        {
            return new CharacterType(this.Client.Account.Characters.Where(x => !x.DeletedDate.HasValue).ToList().IndexOf(this), Name, Id, Position.Map.SceneId, Sex ? (byte)1 : (byte)0,
                new PositionType(Position.X, Position.Y, Position.Z), Level, Job, Stats[DefineEnum.STR].Total, Stats[DefineEnum.STA].Total, Stats[DefineEnum.DEX].Total,
                Stats[DefineEnum.INT].Total, Stats[DefineEnum.SPI].Total, HairMesh, HairColor, HeadMesh, 0, 0, Inventory.Items.Count,
                Inventory.Items.Values.Select(x => x.GetNetwork()).ToArray(), Inventory.ClosetItems.Values.Select(x => x.GetNetwork()).ToArray());
        }
    }
}
