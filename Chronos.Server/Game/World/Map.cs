using Chronos.Protocol;
using Chronos.Protocol.Messages.Snapshots;
using Chronos.Protocol.Types;
using Chronos.Server.Databases.Maps;
using Chronos.Server.Game.Actors;
using Chronos.Server.Game.Actors.Context.Characters;
using Chronos.Server.Handlers.Roleplay;
using Chronos.Server.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Server.Game.World
{
    public class Map
    {
        public Map(MapRecord record)
        {
            Clients = new SimpleClientCollection();
            Objects = new List<WorldObject>();
            Record = record;
        }
        public SimpleClientCollection Clients { get; }
        public List<WorldObject> Objects { get; set; }
        public MapRecord Record { get; }
        public int SceneId
        {
            get
            {
                return Record.Id;
            }
        }

        public void Enter(WorldObject obj)
        {
            foreach (SimpleClient client in Clients)
            {
                ContextRoleplayHandler.SendSnapshotMessage(client, new Snapshot[] { new AddObjectSnapshot(obj.GetObjectType()) });
            }

            if (obj is Character)
            {
                Clients.Add((obj as Character).Client);
            }

            Objects.Add(obj);
            obj.Position.Map = this;
        }
        public void Leave(WorldObject obj)
        {
            if (!Objects.Contains(obj))
                return;

            if (obj is Character)
            {
                Clients.Remove((obj as Character).Client);
            }

            Objects.Remove(obj);

            foreach (SimpleClient client in Clients)
            {
                ContextRoleplayHandler.SendSnapshotMessage(client, new Snapshot[] { new RemoveObjectSnapshot((uint)obj.GetHashCode()) });
            }
        }
        public List<SimpleClient> GetClientsNear(Character character)
        {
            return Clients.Where(x => x != character.Client /*&& character.Position.IsInRange(x.Character.Position, 50)*/).ToList();
        }
    }
}