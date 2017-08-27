using Chronos.Server.Databases.Items;
using Chronos.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Game.Inventories.Items
{
    public class PlayerItem
    {
        public ItemRecord Record { get; }
        public PlayerItem(ItemRecord record)
        {
            Record = record;
        }
        public int Id
        {
            get { return Record.Id; }
            set { Record.Id = value; }
        }
        public int OwnerId
        {
            get { return Record.OwnerId; }
            set { Record.OwnerId = value; }
        }

        public byte Slot
        {
            get { return Record.Slot; }
            set { Record.Slot = value; }
        }
        public uint ItemId
        {
            get { return Record.ItemId; }
            set { Record.ItemId = value; }
        }
        public uint Word
        {
            get { return Record.Word; }
            set { Record.Word = value; }
        }
        public int Option
        {
            get { return Record.Option; }
            set { Record.Option = value; }
        }
        public uint Quantity
        {
            get { return Record.Quantity; }
            set { Record.Quantity = value; }
        }
        public ItemType GetNetwork()
        {
            return new ItemType(Slot, ItemId, Word, Option);
        }
    }
}
