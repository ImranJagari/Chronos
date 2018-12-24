using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using Chronos.Protocol.Types;

namespace Chronos.Protocol.Messages.Snapshots
{
    public class CreateItemSnapshot : Snapshot
    {
        public uint objId;
        public uint bagId;
        public ItemElementType element;
        public short[] objIds;
        public short[] nums;

        public CreateItemSnapshot(uint objId, uint bagId, ItemElementType element, short[] objIds, short[] nums)
        {
            this.objId = objId;
            this.bagId = bagId;
            this.element = element;
            this.objIds = objIds;
            this.nums = nums;
        }

        public override SnapshotTypeEnum SnapshotType => SnapshotTypeEnum.CREATEITEM;

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUInt(objId);
            writer.WriteUInt(bagId);
            element.Serialize(writer);
            writer.WriteShort((short)objIds.Length);
            for (int i = 0; i < objIds.Length; i++)
            {
                writer.WriteShort(objIds[i]);
            }

            for (int i = 0; i < objIds.Length; i++)
            {
                writer.WriteShort(nums[i]);
            }
        }
    }
}
