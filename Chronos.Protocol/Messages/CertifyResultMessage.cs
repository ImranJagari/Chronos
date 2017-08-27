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
    public class CertifyResultMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.CERTIFYRESULT;
        public ErrorEnum errorCode;
        public int need_box_ok;
        public bool unknownBoolean;
        public ErrorEnum subErrorCode;
        public override ushort MessageId => (ushort)Header;
        public CertifyResultMessage() { }
        public CertifyResultMessage(ErrorEnum errorCode, int need_box_ok, bool unknownBoolean, ErrorEnum subErrorCode)
        {
            this.errorCode = errorCode;
            this.need_box_ok = need_box_ok;
            this.unknownBoolean = unknownBoolean;
            this.subErrorCode = subErrorCode;
        }
        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {

            if(errorCode == ErrorEnum.CERT_OK)
            {
                writer.WriteUInt((uint)errorCode);
                writer.WriteInt(need_box_ok);
                writer.WriteBoolean(unknownBoolean);
            }
            else if (errorCode == ErrorEnum.ERR_CERT_VERSION)
            {
                writer.WriteUInt((uint)subErrorCode);
            }
            else
            {
                writer.WriteUInt((uint)errorCode);
                writer.WriteUInt((uint)subErrorCode);
            }
        }
    }
}
