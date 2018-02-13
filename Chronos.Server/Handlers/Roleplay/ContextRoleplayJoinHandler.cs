using Chronos.Core.Extensions;
using Chronos.Protocol;
using Chronos.Protocol.Enums;
using Chronos.Protocol.Messages;
using Chronos.Protocol.Messages.Snapshots;
using Chronos.Protocol.Types;
using Chronos.Protocol.Types.ObjectsType;
using Chronos.Server.Game.Actors.Context.Characters;
using Chronos.Server.Game.Stats;
using Chronos.Server.Game.World;
using Chronos.Server.Manager.Maps;
using Chronos.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Chronos.Server.Handlers.Roleplay
{
    public partial class ContextRoleplayHandler
    {
        
        [HeaderPacket(HeaderEnum.JOIN)]
        public static void HandleJoinMessage(SimpleClient client, JoinMessage message)
        {
            Character character = client.Account.Characters.FirstOrDefault(x => x.Id == message.characterId);
            if(character == null)
            {
                client.Disconnect();
                return;
            }

            client.Character = character;
            client.Character.Client = client;

            Map map = WorldManager.Instance.GetMapById(character.Record.SceneId);
            if(map == null)
            {
                client.Disconnect();
                return;
            }

            client.Character.Position = new ObjectPosition(character.Record.X, character.Record.Y, character.Record.Z);

            map.Enter(client.Character);

            SendJoinRightMessage(client, message.characterId, character.Position.Map.SceneId, character.Position.X, character.Position.Y, character.Position.Z, false, 0);//Send all players in the map

            SendSnapshotMessage(client, new Snapshot[] { new SetStateVerSnapshot(1),
                new UpdateServerTimeSnapshot(DateTime.UtcNow.GetUnixTimeStamp()),
                new AddObjectSnapshot(client.Character.GetObjectType(true))
            });

            List<Snapshot> spawnOtherObjects = new List<Snapshot>();
            foreach(var @object in map.Objects.Where(x => x != character))
            {
                spawnOtherObjects.Add(new AddObjectSnapshot(@object.GetObjectType()));
            }
            SendSnapshotMessage(client, spawnOtherObjects.ToArray());

            //foreach (var stats in client.Character.Stats.Fields)
            //{
            //    SendSnapshotMessage(client, new Snapshot[] { new SetValueObjectSnapshot((uint)client.Character.GetHashCode(), (short)stats.Key, (int)stats.Value.Total) });
            //}
        }
        public static void SendJoinRightMessage(IPacketInterceptor client, int characterId, int sceneId, float x, float y, float z, bool isFestival, uint festivalDay)
        {
            client.Send(new JoinRightMessage(characterId, sceneId, new PositionType(x, y, z), isFestival, festivalDay));
        }
    }
}