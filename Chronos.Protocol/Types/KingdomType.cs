using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Types
{
    public class KingdomType
    {
        public byte kingdom_job;
        public byte kingdom_sub_job;
        public uint kingdom_familyId;
        public string kingdom_familyName;
        public byte kingdom_familyJob;
        public byte titleId;
        public uint family_popular;
        public uint family_rank;
        public byte family_iconId;
        public uint allianceId;
        public byte alliance_job;
        public string alliance_name;
        public KingdomType(byte kingdom_job, byte kingdom_sub_job, uint kingdom_familyId, string kingdom_familyName, byte kingdom_familyJob, byte titleId, uint family_popular,
            uint family_rank, byte family_iconId, uint allianceId, byte alliance_job, string alliance_name)
        {
            this.kingdom_job = kingdom_job;
            this.kingdom_sub_job = kingdom_sub_job;
            this.kingdom_familyId = kingdom_familyId;
            this.kingdom_familyName = kingdom_familyName;
            this.kingdom_familyJob = kingdom_familyJob;
            this.titleId = titleId;
            this.family_popular = family_popular;
            this.family_rank = family_rank;
            this.family_iconId = family_iconId;
            this.allianceId = allianceId;
            this.alliance_job = alliance_job;
            this.alliance_name = alliance_name;
        }

        public void Serialize(IDataWriter writer)
        {
            writer.WriteByte(kingdom_job);
            writer.WriteByte(kingdom_sub_job);
            writer.WriteUInt(kingdom_familyId);
            writer.WriteUTF(kingdom_familyName);
            writer.WriteByte(kingdom_familyJob);
            writer.WriteByte(titleId);
            writer.WriteUInt(family_popular);
            writer.WriteUInt(family_rank);
            writer.WriteByte(family_iconId);
            writer.WriteUInt(allianceId);
            writer.WriteByte(alliance_job);
            writer.WriteUTF(alliance_name);
        }
    }
}