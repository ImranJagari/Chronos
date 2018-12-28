using Chronos.Protocol.Types;
using Chronos.Server.Databases.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Game.Inventories.Items
{
    public class ClosetItem
    {
        public ClosetItemRecord Record { get; }
        public ClosetItem(ClosetItemRecord record)
        {
            Record = record;
        }
        public int Id
        {
            get { return Record.Id; }
            set { Record.Id = value; }
        }
        public int ClosetItemId
        {
            get { return Record.ClosetItemId; }
            set { Record.ClosetItemId = value; }
        }
        public int Slot
        {
            get { return Record.Slot; }
            set { Record.Slot = value; }
        }
        public ClosetItemType GetClosetItemType()
        {
            return new ClosetItemType(ClosetItemId);
        }
        public FateClosetType GetFateClosetType()
        {
            return new FateClosetType(Slot, ClosetItemId, 1, Record.Equipped ? 1 : 0, new int[16]);
        }
    }
}
