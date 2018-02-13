using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Types
{
    public class AdventType
    {
        public const byte MAX_ADVENT_BADGE_IDX = 8;
        public const uint MAX_ADVENT_EXP = 2000000000;
        public const uint MAX_ADVENT_BADGE = 2000000000;
        public int level;
        public int exp;
        public int round;
        public int count;
        public string badge;
        public AdventType(int level, int exp, int round, int count, string badge)
        {
            this.level = level;
            this.exp = exp;
            this.round = round;
            this.count = count;
            this.badge = badge;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(level);
            writer.WriteInt(exp);
            writer.WriteInt(round);
            writer.WriteInt(count);
            writer.WriteInt(1); // IDK ?????
            writer.WriteUTF(badge);
        }
    }
}