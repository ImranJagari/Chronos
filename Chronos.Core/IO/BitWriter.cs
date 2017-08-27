using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Core.IO
{
    public static class BitWriter //TODO: Clean this up.
    {
        private static int RoundUp(int value, int multiple)
        {
            if (value % multiple != 0)
                return value + multiple - value % multiple;

            return value;
        }

        public static byte[] GetBytes(ICollection bits)
        {
            var bytes = new byte[bits.Count / 8];
            bits.CopyTo(bytes, 0);
            return bytes;
        }

        public static BitArray TrimZero(BitArray bits, bool round)
        {
            var size = bits.Count;

            for (; size > 0; size--)
            {
                if (bits[size - 1])
                    break;
            }

            var blah = size;
            if (blah == 0)
                blah = 16;

            var newBits = new BitArray(round ? RoundUp(blah, 16) : blah);

            for (var i = 0; i < size; i++)
                newBits[i] = bits[i];

            return newBits;
        }

        public static BitArray AddFront(BitArray bits, int count)
        {
            var newBits = new BitArray(bits.Count + count);

            for (var i = 0; i < bits.Count; i++)
                newBits[i + count] = bits[i];

            return newBits;
        }

        public static BitArray AddBack(BitArray bits, int count)
        {
            var newBits = new BitArray(bits.Count + count);

            for (var i = 0; i < bits.Count; i++)
                newBits[i] = bits[i];

            return newBits;
        }
    }
}
