using Chronos.Server.Game.Stats;
using Chronos.Server.Game.World;

namespace Chronos.Server.Game.Actors.Context
{
    public abstract class ContextActor : WorldObject, IStatsOwner
    {
        public abstract StatsFields Stats { get; protected set; }
    }
}