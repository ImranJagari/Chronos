using Chronos.Server.Databases.Breeds;
using Chronos.Server.Initialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Server.Manager.Breeds
{
    public class BreedManager : DatabaseManager<BreedManager>
    {
        Dictionary<byte, BreedRecord> m_breeds = new Dictionary<byte, BreedRecord>();
        [Initialization(InitializationPass.First)]
        public override void Initialize()
        {
            m_breeds = Database.Query<BreedRecord>(BreedRecordRelator.FetchQuery).ToDictionary(x => x.Job);
        }

        public BreedRecord GetBreedByJobId(byte jobId)
        {
            if(m_breeds.ContainsKey(jobId))
            {
                return m_breeds[jobId];
            }
            return null;
        }
    }
}