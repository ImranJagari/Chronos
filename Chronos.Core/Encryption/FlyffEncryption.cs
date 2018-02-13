using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Chronos.Core.Encryption
{
    public class FlyffEncryption
    {
        internal static unsafe void LowByte(int* pointer, byte value)
        {
            int num = *pointer >> 8 << 8 | (int)value;
            *pointer = num;
        }

        internal static unsafe void* InitKey(void* key)
        {
            int num1 = (int)((IntPtr)key + 836);
            int num2 = 1024;
            do
            {
                int num3 = *(int*)((IntPtr)key + 24) + *(int*)((IntPtr)key + 300) + *(int*)((IntPtr)key + 576);
                switch (num3)
                {
                    case 0:
                    case 3:
                        int num4 = *(int*)((IntPtr)key + 16);
                        int num5 = (int)(byte)*(int*)((IntPtr)key + 20) + 1 & 63;
                        int num6;
                        LowByte(&num6, (byte)((*(int*)((IntPtr)key + 20) + 1 & 63) - *(int*)((IntPtr)key + 12)));
                        sbyte num7 = (sbyte)(*(int*)((IntPtr)key + 20) + 1 & 63);
                        *(int*)((IntPtr)key + 20) = num5;
                        num6 &= 63;
                        int num8 = (int)num7 - (int)(byte)num4 & 63;
                        *(int*)((IntPtr)key + num5 * 4 + 28) = *(int*)((IntPtr)key + num8 * 4 + 28) + *(int*)((IntPtr)key + num6 * 4 + 28);
                        void* voidPtr1 = key;
                        int num9 = 20;
                        int num10 = *(int*)((IntPtr)voidPtr1 + num9) * 4;
                        uint num11 = (uint)*(int*)((IntPtr)voidPtr1 + num10 + 28);
                        int num12 = num11 < (uint)*(int*)((IntPtr)key + num6 * 4 + 28) || num11 < (uint)*(int*)((IntPtr)key + num8 * 4 + 28) ? 1 : 0;
                        *(int*)((IntPtr)key + 24) = num12;
                        int num13 = *(int*)((IntPtr)key + 292);
                        int num14 = (int)(byte)*(int*)((IntPtr)key + 296) + 1 & 63;
                        int num15;
                        LowByte(&num15, (byte)((*(int*)((IntPtr)key + 296) + 1 & 63) - *(int*)((IntPtr)key + 288)));
                        sbyte num16 = (sbyte)(*(int*)((IntPtr)key + 296) + 1 & 63);
                        *(int*)((IntPtr)key + 296) = num14;
                        num15 &= 63;
                        int num17 = (int)num16 - (int)(byte)num13 & 63;
                        *(int*)((IntPtr)key + num14 * 4 + 304) = *(int*)((IntPtr)key + num17 * 4 + 304) + *(int*)((IntPtr)key + num15 * 4 + 304);
                        void* voidPtr2 = key;
                        int num18 = 296;
                        int num19 = *(int*)((IntPtr)voidPtr2 + num18) * 4;
                        uint num20 = (uint)*(int*)((IntPtr)voidPtr2 + num19 + 304);
                        uint num21 = num20 < (uint)*(int*)((IntPtr)key + num15 * 4 + 304) || num20 < (uint)*(int*)((IntPtr)key + num17 * 4 + 304) ? 1U : 0U;
                        *(int*)((IntPtr)key + 300) = (int)num21;
                        break;
                    default:
                        int num22 = 0;
                        if (num3 == 2)
                            num22 = 1;
                        if (*(int*)((IntPtr)key + 24) == num22)
                        {
                            int num23 = (int)(byte)*(int*)((IntPtr)key + 20) + 1 & 63;
                            int num24;
                            LowByte(&num24, (byte)((*(int*)((IntPtr)key + 20) + 1 & 63) - *(int*)((IntPtr)key + 12)));
                            int num25 = *(int*)((IntPtr)key + 16);
                            *(int*)((IntPtr)key + 20) = num23;
                            num24 &= 63;
                            int num26 = (int)(byte)num23 - (int)(byte)num25 & 63;
                            *(int*)((IntPtr)key + num23 * 4 + 28) = *(int*)((IntPtr)key + num26 * 4 + 28) + *(int*)((IntPtr)key + num24 * 4 + 28);
                            void* voidPtr3 = key;
                            int num27 = 20;
                            int num28 = *(int*)((IntPtr)voidPtr3 + num27) * 4;
                            uint num29 = (uint)*(int*)((IntPtr)voidPtr3 + num28 + 28);
                            int num30 = num29 < (uint)*(int*)((IntPtr)key + num24 * 4 + 28) || num29 < (uint)*(int*)((IntPtr)key + num26 * 4 + 28) ? 1 : 0;
                            *(int*)((IntPtr)key + 24) = num30;
                        }
                        if (*(int*)((IntPtr)key + 300) == num22)
                        {
                            int num23 = (int)(byte)*(int*)((IntPtr)key + 296) + 1 & 63;
                            int num24;
                            LowByte(&num24, (byte)((*(int*)((IntPtr)key + 296) + 1 & 63) - *(int*)((IntPtr)key + 288)));
                            int num25 = *(int*)((IntPtr)key + 292);
                            *(int*)((IntPtr)key + 296) = num23;
                            num24 &= 63;
                            int num26 = (int)(byte)num23 - (int)(byte)num25 & 63;
                            *(int*)((IntPtr)key + num23 * 4 + 304) = *(int*)((IntPtr)key + num26 * 4 + 304) + *(int*)((IntPtr)key + num24 * 4 + 304);
                            void* voidPtr3 = key;
                            int num27 = 296;
                            int num28 = *(int*)((IntPtr)voidPtr3 + num27) * 4;
                            uint num29 = (uint)*(int*)((IntPtr)voidPtr3 + num28 + 304);
                            int num30 = num29 < (uint)*(int*)((IntPtr)key + num24 * 4 + 304) || num29 < (uint)*(int*)((IntPtr)key + num26 * 4 + 304) ? 1 : 0;
                            *(int*)((IntPtr)key + 300) = num30;
                        }
                        if (*(int*)((IntPtr)key + 576) != num22)
                            goto label_11;
                        else
                            break;
                }
                int num31 = (int)(byte)*(int*)((IntPtr)key + 572) + 1 & 63;
                *(int*)((IntPtr)key + 572) = num31;
                int num32 = (int)(byte)num31 - (int)(byte)*(int*)((IntPtr)key + 564) & 63;
                int num33 = (int)(byte)num31 - (int)(byte)*(int*)((IntPtr)key + 568) & 63;
                *(int*)((IntPtr)key + num31 * 4 + 580) = *(int*)((IntPtr)key + num33 * 4 + 580) + *(int*)((IntPtr)key + num32 * 4 + 580);
                void* voidPtr4 = key;
                int num34 = 572;
                int num35 = *(int*)((IntPtr)voidPtr4 + num34) * 4;
                uint num36 = (uint)*(int*)((IntPtr)voidPtr4 + num35 + 580);
                int num37 = num36 < (uint)*(int*)((IntPtr)key + num32 * 4 + 580) || num36 < (uint)*(int*)((IntPtr)key + num33 * 4 + 580) ? 1 : 0;
                *(int*)((IntPtr)key + 576) = num37;
                label_11:
                int num38 = num1;
                void* voidPtr5 = key;
                int num39 = 296;
                int num40 = *(int*)((IntPtr)voidPtr5 + num39) * 4;
                int num41 = *(int*)((IntPtr)voidPtr5 + num40 + 304);
                void* voidPtr6 = key;
                int num42 = 572;
                int num43 = *(int*)((IntPtr)voidPtr6 + num42) * 4;
                int num44 = *(int*)((IntPtr)voidPtr6 + num43 + 580);
                int num45 = num41 ^ num44;
                void* voidPtr7 = key;
                int num46 = 20;
                int num47 = *(int*)((IntPtr)voidPtr7 + num46) * 4;
                int num48 = *(int*)((IntPtr)voidPtr7 + num47 + 28);
                int num49 = num45 ^ num48;
                *(int*)num38 = num49;
                num1 += 4;
                --num2;
            }
            while (num2 != 0);
            *(int*)((IntPtr)key + 4) = 0;
            return key;
        }

        internal static unsafe int EncryptDecrypt(void* key, byte* data, int length)
        {
            int num1 = length;
            int num2 = 0;
            if (length > 0)
            {
                byte* voidPtr1 = data;
                if ((IntPtr)data != IntPtr.Zero)
                {
                    do
                    {
                        int num3 = *(int*)((IntPtr)key + 4);
                        int num4 = 4096 - num3;
                        if (4096 - num3 > 0)
                        {
                            if (num4 > num1)
                                num4 = num1;
                            int num5 = num1 - num4;
                            int num6 = 0;
                            sbyte* numPtr = (sbyte*)((IntPtr)key + num3 + 836);
                            if (num4 - 3 > 0)
                            {
                                uint num7 = ((uint)(num4 - 4) >> 2) + 1U;
                                num6 = (int)num7 * 4;
                                do
                                {
                                    void* voidPtr2 = voidPtr1;
                                    int num8 = *(int*)voidPtr2 ^ *(int*)numPtr;
                                    *(int*)voidPtr2 = num8;
                                    voidPtr1 += 4;
                                    numPtr += 4;
                                    --num7;
                                }
                                while ((int)num7 != 0);
                            }
                            if (num6 < num4)
                            {
                                int num7 = num4 - num6;
                                do
                                {
                                    void* voidPtr2 = voidPtr1;
                                    int num8 = (int)*(byte*)voidPtr2 ^ (int)*numPtr;
                                    *(sbyte*)voidPtr2 = (sbyte)num8;
                                    ++voidPtr1;
                                    ++numPtr;
                                    --num7;
                                }
                                while (num7 != 0);
                            }
                            num1 = num5;
                            num2 = num4 + *(int*)((IntPtr)key + 4);
                            *(int*)((IntPtr)key + 4) = num2;
                        }
                        else
                            num2 = (int)InitKey(key);
                    }
                    while (num1 > 0);
                }
            }
            return num2;
        }

        internal static unsafe int GenerateKey(int seed, byte* realKey)
        {
            byte* numPtr1 = realKey;
            *(int*)realKey = seed ^ 84048153;
            *(int*)(realKey + 8) = seed ^ 84048153;
            *(int*)(realKey + 8) = (int)((uint)(seed ^ 84048153) >> 1) | (seed ^ 84048153 ^ (seed ^ 84048153 ^ (seed ^ 84048153 ^ (seed ^ 84048153 ^ (seed ^ 84048153 ^ (seed ^ 84048153) * 2) * 2) * 4) * 4) << 25) & int.MinValue;
            int num1 = *(int*)numPtr1 * 2 ^ (*(int*)numPtr1 * 2 ^ (int)((uint)*(int*)numPtr1 >> 1)) & 1431655765;
            *(int*)(realKey + 284) = num1;
            *(int*)(realKey + 12) = 55;
            *(int*)(realKey + 16) = 24;
            IntPtr num2 = (IntPtr)(realKey + 284);
            int num3 = (int)((uint)num1 >> 1);
            int num4 = num1;
            int num5 = num1;
            int num6 = num1;
            int num7 = 2;
            int num8 = num6 * num7;
            int num9 = (num6 ^ num8) * 2;
            int num10 = (num5 ^ num9) * 4;
            int num11 = (num5 ^ num10) * 4;
            int num12 = (num4 ^ num11) << 25;
            int num13 = (num4 ^ num12) & int.MinValue;
            int num14 = num3 | num13;
            *(int*)num2 = num14;
            int num15 = ~(*(int*)numPtr1 * 16 ^ (*(int*)realKey * 16 ^ (int)((uint)*(int*)numPtr1 >> 4)) & 252645135);
            *(int*)(realKey + 560) = num15;
            *(int*)(realKey + 288) = 57;
            *(int*)(realKey + 292) = 7;
            IntPtr num16 = (IntPtr)(realKey + 560);
            int num17 = (int)((uint)num15 >> 1);
            int num18 = num15;
            int num19 = num15;
            int num20 = num15;
            int num21 = 2;
            int num22 = num20 * num21;
            int num23 = (num20 ^ num22) * 2;
            int num24 = (num19 ^ num23) * 4;
            int num25 = (num19 ^ num24) * 4;
            int num26 = (num18 ^ num25) << 25;
            int num27 = (num18 ^ num26) & int.MinValue;
            int num28 = num17 | num27;
            *(int*)num16 = num28;
            *(int*)(realKey + 564) = 58;
            *(int*)(realKey + 568) = 19;
            byte* numPtr2 = realKey + 28;
            int num29 = 3;
            int num30;
            do
            {
                int num31 = *(int*)(numPtr2 - 20);
                byte* numPtr3 = numPtr2;
                int num32 = 64;
                do
                {
                    int num33 = 32;
                    do
                    {
                        int num34 = num31;
                        int num35 = num31;
                        int num36 = num31;
                        int num37 = 2;
                        int num38 = num36 * num37;
                        int num39 = (num36 ^ num38) * 2;
                        int num40 = (num35 ^ num39) * 4;
                        int num41 = (num35 ^ num40) * 4;
                        int num42 = (num34 ^ num41) << 25;
                        num31 = (num34 ^ num42) & int.MinValue | (int)((uint)num31 >> 1);
                        --num33;
                    }
                    while (num33 != 0);
                    *(int*)numPtr3 = num31;
                    numPtr3 += 4;
                    --num32;
                }
                while (num32 != 0);
                *(int*)(numPtr2 - 4) = 0;
                *(int*)(numPtr2 - 8) = 63;
                numPtr2 += 276;
                num30 = num29 - 1;
                num29 = num30;
            }
            while (num30 != 0);
            *(int*)(realKey + 4) = 4096;
            return num30;
        }
    }
    public class KeyPair : IDisposable
    {
        private unsafe byte* encryptionKey;
        private unsafe byte* decryptionKey;
        public readonly int Seed;

        public unsafe KeyPair(int seed)
        {
            this.Seed = seed;
            fixed (byte* data = new byte[(uint)ushort.MaxValue])
            {
                FlyffEncryption.GenerateKey(seed, data);
                encryptionKey = data;
            }
            fixed (byte* data = new byte[(uint)ushort.MaxValue])
            {
                FlyffEncryption.GenerateKey(seed, data);
                decryptionKey = data;
            }
        }

        public unsafe int Encrypt(ref byte[] data, int offset, int size)
        {
            fixed (byte* data1 = &data[offset])
                return FlyffEncryption.EncryptDecrypt((void*)this.encryptionKey, data1, size);
        }

        public unsafe int Encrypt(ref byte[] data)
        {
            fixed (byte* data1 = &data[0])
                return FlyffEncryption.EncryptDecrypt((void*)this.encryptionKey, data1, data.Length);
        }

        public unsafe int Decrypt(ref byte[] data, int offset, int size)
        {
            fixed (byte* data1 = &data[offset])
                return FlyffEncryption.EncryptDecrypt((void*)this.decryptionKey, data1, size);
        }

        public unsafe int Decrypt(ref byte[] data)
        {
            fixed (byte* data1 = &data[0])
                return FlyffEncryption.EncryptDecrypt((void*)this.decryptionKey, data1, data.Length);
        }

        private unsafe void Destroy()
        {
            Marshal.FreeHGlobal((IntPtr)encryptionKey);
            Marshal.FreeHGlobal((IntPtr)decryptionKey);
        }

        protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
        {
            if (A_0)
            {
                this.Destroy();
            }
            else
            {
                this.Dispose();
            }
        }

        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }
    }
}