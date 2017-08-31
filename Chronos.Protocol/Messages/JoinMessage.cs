using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using static Chronos.Protocol.MessageReceiver;

namespace Chronos.Protocol.Messages
{
    public class JoinMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.JOIN;
        public int characterId;
        public byte patch_version;
        public string hd_info;
        public override ushort MessageId => (ushort)Header;

        public override void Deserialize(IDataReader reader)
        {
            characterId = reader.ReadInt();
            patch_version = reader.ReadByte();
            hd_info = reader.ReadUTF();
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.SerializeException;
        }
    }
}
