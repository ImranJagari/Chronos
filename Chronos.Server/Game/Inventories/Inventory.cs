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
        }
    }
}
