using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Types.ObjectsType
{
    public class ObjectType
    {
        public ObjectTypeEnum objectType;
        public uint objectId;

        public uint id;
        public uint linkId; // ??? linked to what ???

        public float x, y, z, angle, angleX;
        public short scale;

        public ObjectType(ObjectTypeEnum objectType, uint objectId, uint id, uint linkId, float x, float y, float z,
            float angle, float angleX, short scale)
        {
            this.objectType = objectType;
            this.objectId = objectId;
            this.id = id;
            this.linkId = linkId;
            this.x = x;
            this.y = y;
            this.z = z;
            this.angle = angle;
            this.angleX = angleX;
            this.scale = scale;
        }

        public virtual void SerializeNeverChange(IDataWriter writer)
        {
            writer.WriteUInt((uint)objectType);
            writer.WriteUInt(objectId);
        }
        public virtual void SerializeSometimesChange(IDataWriter writer)
        {
            writer.WriteUInt(id);
            writer.WriteUInt(linkId);
        }
        public virtual void SerializeAlwaysChange(IDataWriter writer)
        {
            writer.WriteFloat(x);
            writer.WriteFloat(y);
            writer.WriteFloat(z);
            writer.WriteFloat(angle);
            writer.WriteFloat(angleX);
            writer.WriteShort(scale);
        }
    }
}