using System;
using System.IO;
using System.Text;

namespace Chronos.Core.IO
{
    public class BigEndianReader : IDataReader
    {
        private BinaryReader m_reader;

        public BigEndianReader()
        {
            m_reader = new BinaryReader(new MemoryStream(), Encoding.UTF8);
        }

        public BigEndianReader(Stream stream)
        {
            m_reader = new BinaryReader(stream, Encoding.UTF8);
        }

        //public BigEndianReader(BufferSegment segment)
        //{
        //    m_reader = new BinaryReader(new MemoryStream(segment.Buffer.Array), Encoding.UTF8);
        //}

        public BigEndianReader(byte[] tab)
        {
            m_reader = new BinaryReader(new MemoryStream(tab), Encoding.UTF8);
        }
        public byte[] Data
        {
            get
            {
                long position = this.BaseStream.Position;
                byte[] buffer = new byte[this.BaseStream.Length];
                this.BaseStream.Position = 0L;
                this.BaseStream.Read(buffer, 0, (int)this.BaseStream.Length);
                this.BaseStream.Position = position;
                return buffer;
            }
        }
        public Stream BaseStream => m_reader.BaseStream;

        public long BytesAvailable => m_reader.BaseStream.Length - m_reader.BaseStream.Position;

        public long Position => m_reader.BaseStream.Position;

        public short ReadShort()
        {
            return m_reader.ReadInt16();
        }

        public int ReadInt()
        {
            return m_reader.ReadInt32();
        }

        public long ReadLong()
        {
            return m_reader.ReadInt64();
        }

        public float ReadFloat()
        {
            return m_reader.ReadSingle();
        }

        public ushort ReadUShort()
        {
            return m_reader.ReadUInt16();
        }

        public uint ReadUInt()
        {
            return m_reader.ReadUInt32();
        }

        public ulong ReadULong()
        {
            return m_reader.ReadUInt64();
        }

        public byte ReadByte()
        {
            return m_reader.ReadByte();
        }

        public sbyte ReadSByte()
        {
            return m_reader.ReadSByte();
        }

        public byte[] ReadBytes(int n)
        {
            return m_reader.ReadBytes(n);
        }

        public bool ReadBoolean()
        {
            return m_reader.ReadByte() == 1;
        }

        public char ReadChar()
        {
            return (char) ReadUShort();
        }

        public double ReadDouble()
        {
            return m_reader.ReadDouble();
        }

        public string ReadUTF()
        {
            return new String(m_reader.ReadChars(count: m_reader.ReadUInt16()));
        }

        public string ReadUTFBytes(ushort len)
        {
            var bytes = ReadBytes(len);
            return Encoding.UTF8.GetString(bytes);
        }

        public void SkipBytes(int n)
        {
            for (var i = 0; i < n; i++)
            {
                m_reader.ReadByte();
            }
        }

        public void Seek(int offset, SeekOrigin seekOrigin)
        {
            m_reader.BaseStream.Seek(offset, seekOrigin);
        }

        public void Dispose()
        {
            m_reader.Dispose();
            m_reader = null;
        }

        private byte[] ReadBigEndianBytes(int count)
        {
            var array = new byte[count];
            for (var i = count - 1; i >= 0; i--)
            {
                array[i] = (byte) BaseStream.ReadByte();
            }
            return array;
        }

        public BigEndianReader ReadBytesInNewBigEndianReader(int n)
        {
            return new BigEndianReader(m_reader.ReadBytes(n));
        }

        public float ReadSingle()
        {
            return BitConverter.ToSingle(ReadBigEndianBytes(4), 0);
        }

        public string ReadUTF7BitLength()
        {
            var n = ReadInt();
            var bytes = ReadBytes(n);
            return Encoding.UTF8.GetString(bytes);
        }

        public void Add(byte[] data, int offset, int count)
        {
            var position = m_reader.BaseStream.Position;
            m_reader.BaseStream.Position = m_reader.BaseStream.Length;
            m_reader.BaseStream.Write(data, offset, count);
            m_reader.BaseStream.Position = position;
        }
    }
}