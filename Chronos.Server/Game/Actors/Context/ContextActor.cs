using Chronos.Protocol.Types.ObjectsType;
using Chronos.Server.Game.Stats;
using Chronos.Server.Game.World;
using System.Linq;

namespace Chronos.Server.Game.Actors.Context
{
    public abstract class ContextActor : WorldObject, IStatsOwner
    {
        public abstract StatsFields Stats { get; protected set; }
        public abstract string Name
        {
            get;
            set;
        }
        public override ObjectType GetObjectType(bool me = false)
        {
            return new SpriteObjectType(ObjectType, (uint)Id, 11, 0xFFFFFFFF, Position.X, Position.Y, Position.Z, 0, 0,
                DEFAULT_SCALE, Name, Stats.Fields.Count, Stats.Fields.Keys.Select(x => (ushort)x).ToArray(),
                Stats.Fields.Values.Select(x => x.Total).ToArray(), 0, new byte[0], new int[0], new int[0]);
        }
    }
}