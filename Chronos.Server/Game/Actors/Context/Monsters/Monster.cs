using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Protocol.Enums;
using Chronos.Protocol.Types.ObjectsType;
using Chronos.Server.Game.Stats;

namespace Chronos.Server.Game.Actors.Context.Monsters
{
    public class Monster : ContextActor
    {
        public override ObjectTypeEnum ObjectType => ObjectTypeEnum.OT_MOB;
        public override int Id { get; protected set; }
        public uint MonsterId { get; private set; }
        public override StatsFields Stats { get; protected set; }
        public override string Name { get; set; }

        public override ObjectType GetObjectType(bool me = false)
        {
            return new MonsterObjectType(ObjectType, (uint)GetHashCode(), MonsterId, 0xFFFFFFFF, Position.X, Position.Y, Position.Z, 0, 0, 100,
                Name, Stats.Fields.Count, Stats.Fields.Keys.Select(x => (ushort)x).ToArray(), Stats.Fields.Values.Select(x => x.Total).ToArray(),
                0, new byte[0], new int[0], new int[0], 0, false, 0, 0, 0, 0, 0, new uint[0], 0, true, true, false, 0, 0, 0, -1, 0);
        }
    }
}
