using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*--------------------------------------------------------
 * HashAlgorithm.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 15/07/2014 20:24:48
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindsoulDataFile
{
    public static class HashAlgorithm
    {
        public static UInt32 Hash(this String input)
        {
            //x86 - 32 bits - Registers
            UInt32 eax, ebx, ecx, edx, edi, esi;
            UInt64 num = 0;

            UInt32 v;
            Int32 i;
            UInt32[] m = new UInt32[0x46];
            Byte[] buffer = new Byte[0x100];

            input = input.ToLowerInvariant();
            //input = input.Replace('\\', '/');

            for (i = 0; i < input.Length; i++)
                buffer[i] = (Byte)input[i];

            Int32 Length = (input.Length % 4 == 0 ? input.Length / 4 : input.Length / 4 + 1);
            for (i = 0; i < Length; i++)
                m[i] = BitConverter.ToUInt32(buffer, i * 4);

            m[i++] = 0x9BE74448; //
            m[i++] = 0x66F42C48; //

            v = 0xF4FA8928; //

            edi = 0x7758B42B;
            esi = 0x37A8470E; //

            for (ecx = 0; ecx < i; ecx++)
            {
                ebx = 0x267B0B11; //
                v = (v << 1) | (v >> 0x1F);
                ebx ^= v;
                eax = m[ecx];
                esi ^= eax;
                edi ^= eax;
                edx = ebx;
                edx += edi;
                edx |= 0x02040801; // 
                edx &= 0xBFEF7FDF;//
                num = edx;
                num *= esi;
                eax = (UInt32)num;
                edx = (UInt32)(num >> 0x20);
                if (edx != 0)
                    eax++;
                num = eax;
                num += edx;
                eax = (UInt32)num;
                if (((UInt32)(num >> 0x20)) != 0)
                    eax++;
                edx = ebx;
                edx += esi;
                edx |= 0x00804021; //
                edx &= 0x7DFEFBFF; //
                esi = eax;
                num = edi;
                num *= edx;
                eax = (UInt32)num;
                edx = (UInt32)(num >> 0x20);
                num = edx;
                num += edx;
                edx = (UInt32)num;
                if (((UInt32)(num >> 0x20)) != 0)
                    eax++;
                num = eax;
                num += edx;
                eax = (UInt32)num;
                if (((UInt32)(num >> 0x20)) != 0)
                    eax += 2;
                edi = eax;
            }
            esi ^= edi;
            v = esi;

            return v;
        }
    }
}
