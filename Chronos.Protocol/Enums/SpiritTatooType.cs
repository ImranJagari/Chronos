using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Enums
{
    public class SpiritTatooType
    {
        public string serializedSpiritTatoo;
        public SpiritTatooType(string serializedSpiritTatoo)
        {
            this.serializedSpiritTatoo = serializedSpiritTatoo;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(serializedSpiritTatoo);
        }
    }
}