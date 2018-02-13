using Chronos.Core.Encryption;
using Chronos.Core.IO;
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

        public bool Build(ref BigEndianReader reader, KeyPair keyPairDecryption)
        {
            if(reader.Data.Length < 1)
            {
                return false;
            }

            byte[] data = reader.Data;
            keyPairDecryption.Decrypt(ref data, 0, reader.Data.Length);

            CompleteData = data;

            reader = new BigEndianReader(data);
            Length = reader.ReadUInt();
            Header = reader.ReadUShort();
            Data = new byte[reader.BytesAvailable];
            int j = 0;
            for(long i = reader.Position; i < Data.Length; i++)
            {
                Data[j] = reader.Data[i];
                j++;
            }
            //reader.Data.CopyTo(Data, sizeof(uint) + sizeof(ushort) - 1);
            return true;
        }
    }
}
