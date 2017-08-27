using Chronos.Server.Databases.Items;
using Chronos.Server.Game.Inventories.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Manager.Items
{
    public class ItemManager : DatabaseManager<ItemManager>
    {
        public Dictionary<uint, PlayerItem> GetItemsByOwnerId(int ownerId)
        {
            return Database.Fetch<ItemRecord>(string.Format(ItemRecordRelator.FetchQueryByOwnerId, ownerId)).Select(x => new PlayerItem(x)).ToDictionary(x => x.ItemId);
        }
        public Dictionary<int, ClosetItem> GetClosetItemsByOwnerId(int ownerId)
        {
            return Database.Fetch<ClosetItemRecord>(string.Format(ClosetItemRecordRelator.FetchQueryByOwnerId, ownerId)).Select(x => new ClosetItem(x)).ToDictionary(x => x.ClosetItemId);
        }
    }
}
