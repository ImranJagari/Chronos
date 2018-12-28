using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Databases.Items
{
    public class ItemStorage<T>
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public byte Slot { get; set; }
        public uint ItemId { get; set; }
        public uint Quantity { get; set; }
    }
}
