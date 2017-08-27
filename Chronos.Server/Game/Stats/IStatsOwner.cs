using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Game.Stats
{
    public interface IStatsOwner
    {
        StatsFields Stats { get; }
    }
}
