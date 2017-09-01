using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using static Chronos.Protocol.MessageReceiver;
using Chronos.Protocol.Types;

namespace Chronos.Protocol.Messages
{
    public class JoinRightMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.JOIN_RIGHT;

        public int characterId;
        public int sceneId;
        public PositionType position;
        public bool isFestival;
        public uint festivalDay;

        public override ushort MessageId => (ushort)Header;

        public JoinRightMessage() { }
        public JoinRightMessage(int characterId, int sceneId, PositionType position, bool isFestival, uint festivalDay)
        {
            this.characterId = characterId;
            this.sceneId = sceneId;
            this.position = position;
            this.isFestival = isFestival;
            this.festivalDay = festivalDay;
        }

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(characterId);
            writer.WriteInt(sceneId);
            position.Serialize(writer);
            writer.WriteBoolean(isFestival);
            writer.WriteUInt(festivalDay);
        }
    }
}
