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
    public class DeleteCharacterResultMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.DELETEPLAYERRESULT;

        public ErrorEnum error;
        public int characterId;
        public int flagBlocked;

        public override ushort MessageId => (ushort)Header;
        public DeleteCharacterResultMessage() { }
        public DeleteCharacterResultMessage(ErrorEnum error, int characterId, int flagBlocked)
        {
            this.error = error;
            this.characterId = characterId;
            this.flagBlocked = flagBlocked;
        }
        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)error);
            writer.WriteInt(characterId);
            writer.WriteInt(flagBlocked);
        }
    }
}
