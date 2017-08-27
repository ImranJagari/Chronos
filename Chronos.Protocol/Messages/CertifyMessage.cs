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
    public class CertifyMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.CERTIFY;

        public string username;
        public string password;
        public string hdsn;
        public byte[] ip_key;
        public uint version;
        public uint realVersion;
        public byte op_flag;
        public string otp_pwd;

        public override ushort MessageId => (ushort)Header;
        public CertifyMessage() { }
        public override void Deserialize(IDataReader reader)
        {
            username = reader.ReadUTF();
            password = reader.ReadUTF();
            hdsn = reader.ReadUTF();
            ip_key = reader.ReadBytes(21);
            version = reader.ReadUInt();
            realVersion = reader.ReadUInt();
            op_flag = reader.ReadByte();
            otp_pwd = reader.ReadUTF();
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.DeserializeException;
        }
    }
}
