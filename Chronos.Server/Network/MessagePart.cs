using Chronos.Core.IO;
using FFEncryptionLibrary;
using System;
namespace Chronos.Server.Network
{
    public class MessagePart
    {
        private byte[] m_data;
        private byte[] m_completeData;

        public ushort? Header { get; private set; }

        public ushort? MessageId
        {
            get
            {
                if (!this.Header.HasValue)
                    return new ushort?();
                ushort? header = this.Header;
                return header.HasValue ? header.Value : new ushort?();
            }
        }

        public uint? Length { get; private set; }

        public byte[] Data
        {
            get
            {
                return this.m_data;
            }
            private set
            {
                this.m_data = value;
            }
        }

        public byte[] CompleteData
        {
            get
            {
                return this.m_completeData;
            }
            private set
            {
                this.m_completeData = value;
            }
        }

        public bool Build(BigEndianReader reader, KeyPair keyPairDecryption)
        {
            if(reader.Data.Length < 4)
            {
                return false;
            }

            byte[] data = reader.Data;
            keyPairDecryption.Decrypt(ref data, 0, reader.Data.Length);
            CompleteData = data;
            reader = new BigEndianReader(data);

            Length = reader.ReadUInt();
            Header = reader.ReadUShort();

            Data = reader.Data;
            return true;
        }
    }
}
