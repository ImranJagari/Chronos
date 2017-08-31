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
    public class DeleteCharacterMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.DELETEPLAYER;

        public int characterId;
        public string password;

        public override ushort MessageId => (ushort)Header;

        public DeleteCharacterMessage() { }

        public override void Deserialize(IDataReader reader)
        {
            characterId = reader.ReadInt();
            password = reader.ReadUTF();
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.SerializeException;
        }
    }
}
