using Chronos.Server.Databases.Maps;
using Chronos.Server.Game.World;
using Chronos.Server.Initialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Manager.Maps
{
    public class WorldManager : DatabaseManager<WorldManager>
    {
        private Dictionary<int, Map> m_maps = new Dictionary<int, Map>();

        [Initialization(InitializationPass.Second)]
        public override void Initialize()
        {
            m_maps = Database.Query<MapRecord>(MapRecordRelator.FetchQuery).Select(x => new Map(x)).ToDictionary(x => x.SceneId);
        }

        public Map GetMapById(int sceneId)
        {
            return m_maps.ContainsKey(sceneId) ? m_maps[sceneId] : null;
        }
    }
}
