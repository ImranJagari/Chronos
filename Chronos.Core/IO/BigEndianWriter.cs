using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Chronos.Core.IO
{
    public class BigEndianWriter : IDisposable, IDataWriter
    {
        private BinaryWriter m_writer;
        public Stream BaseStream
        {
            get
            {
                return this.m_writer.BaseStream;
            }
        }
        public long BytesAvailable
        {
            get
            {
                return this.m_writer.BaseStream.Length - this.m_writer.BaseStream.Position;
            }
        }
        public long Position
        {
            get
            {
                return this.m_writer.BaseStream.Position;
            }
            set
            {
                this.m_writer.BaseStream.Position = value;
            }
        }
        public byte[] Data
        {
            get
            {
                long position = this.m_writer.BaseStream.Position;
                byte[] array = new byte[this.m_writer.BaseStream.Length];
                this.m_writer.BaseStream.Position = 0L;
                this.m_writer.BaseStream.Read(array, 0, (int)this.m_writer.BaseStream.Length);
                this.m_writer.BaseStream.Position = position;
                return array;
            }
        }

        public BigEndianWriter()
        {
            this.m_writer = new BinaryWriter(new MemoryStream(), Encoding.UTF8);
        }

        public BigEndianWriter(Stream stream)
        {
            this.m_writer = new BinaryWriter(stream, Encoding.UTF8);
        }

        private void WriteBigEndianBytes(byte[] endianBytes)
        {
            for (int i = endianBytes.Length - 1; i >= 0; i--)
            {
                this.m_writer.Write(endianBytes[i]);
            }
        }

        public void WriteShort(short @short)
        {
            this.WriteBigEndianBytes(BitConverter.GetBytes(@short));
        }

        public void WriteInt(int @int)
        {
            this.WriteBigEndianBytes(BitConverter.GetBytes(@int));
        }

        public void WriteLong(long @long)
        {
            this.WriteBigEndianBytes(BitConverter.GetBytes(@long));
        }

        public void WriteUShort(ushort @ushort)
        {
            this.WriteBigEndianBytes(BitConverter.GetBytes(@ushort));
        }

        public void WriteUInt(uint @uint)
        {
            this.WriteBigEndianBytes(BitConverter.GetBytes(@uint));
        }

        public void WriteULong(ulong @ulong)
        {
            this.WriteBigEndianBytes(BitConverter.GetBytes(@ulong));
        }

        public void WriteByte(byte @byte)
        {
            this.m_writer.Write(@byte);
        }

        public void WriteSByte(sbyte @byte)
        {
            this.m_writer.Write(@byte);
        }

        public void WriteFloat(float @float)
        {
            this.m_writer.Write(@float);
        }

        public void WriteBoolean(bool @bool)
        {
            if (@bool)
            {
                this.m_writer.Write((byte)1);
            }
            else
            {
                this.m_writer.Write((byte)0);
            }
        }

        public void WriteChar(char @char)
        {
            this.WriteBigEndianBytes(BitConverter.GetBytes(@char));
        }

        public void WriteDouble(double @double)
        {
            this.WriteBigEndianBytes(BitConverter.GetBytes(@double));
        }

        public void WriteSingle(float single)
        {
            this.WriteBigEndianBytes(BitConverter.GetBytes(single));
        }

        public void WriteUTF(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            ushort num = (ushort)bytes.Length;
            this.WriteUShort(num);
            for (int i = 0; i < (int)num; i++)
            {
                this.m_writer.Write(bytes[i]);
            }
        }

        public void WriteUTFBytes(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            int num = bytes.Length;
            for (int i = 0; i < num; i++)
            {
                this.m_writer.Write(bytes[i]);
            }
        }

        public void WriteBytes(byte[] data)
        {
            this.m_writer.Write(data);
        }
        public void WriteX(int value)
        {
            if (value == 0)
            {
                WriteUShort(1);

                return;
            }

            BitArray bits;

            var temp = new BitArray(new[] { value });

            temp = BitWriter.TrimZero(temp, false);

            if (temp.Count <= 13)
            {
                bits = BitWriter.TrimZero(BitWriter.AddFront(temp, 2), true);

                bits[0] = true;
            }
            else
            {
                bits = BitWriter.TrimZero(BitWriter.AddFront(temp, 8), true);

                bits[1] = true;
            }

            var bytes = BitWriter.GetBytes(BitWriter.TrimZero(bits, true));

            if (bytes.Length == 4)
                bytes = BitWriter.GetBytes(BitWriter.AddBack(BitWriter.TrimZero(bits, true), 8));

            WriteBytes(bytes);
        }

        public void WriteFormat(string format, params object[] values)
        {
            var formatArray = format.ToLowerInvariant().ToCharArray();

            if (formatArray.Length != values.Length)
                throw new ArgumentException("Format length doesn't match the number of values.");

            for (var i = 0; i < formatArray.Length; i++)
                WriteFormat(formatArray[i], values[i]);
        }

        public void WriteFormat(char format, object value)
        {
            switch (format)
            {
                case 'b':
                    WriteByte((byte)value);
                    break;
                case 'c':
                    WriteSByte((sbyte)value);
                    break;
                case 'f':
                    WriteFloat((float)value);
                    break;
                case 'i':
                    WriteInt((int)value);
                    break;
                case 's':
                    WriteUTF((string)value);
                    break;
                case 'u':
                    WriteUInt((uint)value);
                    break;
                case 'w':
                    WriteUShort((ushort)value);
                    break;
                case 'x':
                    WriteX((int)value);
                    break;
            }
        }
        public void Seek(int offset)
        {
            this.Seek(offset, SeekOrigin.Begin);
        }

        public void Seek(int offset, SeekOrigin seekOrigin)
        {
            this.m_writer.BaseStream.Seek((long)offset, seekOrigin);
        }

        public void Clear()
        {
            this.m_writer = new BinaryWriter(new MemoryStream(), Encoding.UTF8);
        }

        public void Dispose()
        {
            this.m_writer.Flush();
            this.m_writer.Dispose();
            this.m_writer = null;
        }
    }
}