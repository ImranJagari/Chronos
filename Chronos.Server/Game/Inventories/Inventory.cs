using Chronos.Server.Game.Inventories.Items;
using Chronos.Server.Manager.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Game.Inventories
{
    public class Inventory 
    {
        public static readonly ushort[] InventorySlots = new ushort[]
                                                       {

                                                           0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x0A,0x0B,0x0C,0x0D,0x0E,0x0F,0x10,0x11,0x12,0x13,
                                                           0x14,0x15,0x16,0x17,0x18,0x19,0x1A,0x1B,0x1C,0x1D,0x1E,0x1F,0x20,0x21,0x22,0x23,0x24,0x25,0x26,0x27,
                                                           0x28,0x29,0x2A,0x2B,0x42,0x2D,0x2E,0x2C,0x30,0x31,0x32,0x33,0x43,0x35,0x36,0x37,0x38,0x39,0x3A,0x3B,
                                                           0x3C,0x3D,0x3E,0x3F,0x40,0x41,0x42,0x43,0x44,0x45,0x46,0x47,0x48
                                                       };
        public static readonly ushort[] QuestSlots = new ushort[]
                                                   {
                                                       0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B,
                                                       0x0C, 0x0D, 0x0E, 0x0F, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17,
                                                       0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x20, 0x21, 0x22, 0x23,
                                                       0x24, 0x25, 0x26, 0x27, 0x28, 0x29
                                                   };

        public Dictionary<uint, PlayerItem> Items;
        public Dictionary<int, ClosetItem> ClosetItems;

        public Inventory()
        {
            Items = new Dictionary<uint, PlayerItem>();
        }

        public PlayerItem GetItemById(uint itemId) => Items[itemId];
        public ClosetItem GetClosetItemById(int closetItemId) => ClosetItems[closetItemId];

        public void LoadData(int ownerId)
        {
            Items = ItemManager.Instance.GetItemsByOwnerId(ownerId);
            ClosetItems = ItemManager.Instance.GetClosetItemsByOwnerId(ownerId);
            while(ClosetItems.Count < 5)
            {
                ClosetItems.Add(ClosetItems.Count, new ClosetItem(new Databases.Items.ClosetItemRecord()
                {
                    ClosetItemId = 0,
                    Equipped = false,
                    OwnerId = ownerId
                }));
            }
        }
    }
}
