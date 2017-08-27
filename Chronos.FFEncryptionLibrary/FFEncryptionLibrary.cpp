
#include <SDKDDKVer.h>

#define WIN32_LEAN_AND_MEAN
#include <windows.h>

	using namespace System;
using namespace System::Runtime::InteropServices;

void LowByte(int* pointer, Byte value)
{
	int temp = (*pointer >> 8) << 8;
	temp |= value;
	*pointer = temp;
}

void* InitKey(void *key)
{
	int v1; // ebx@1
	int v2; // ecx@2
	signed int v3; // edi@4
	int v4; // ecx@7
	int v5; // edx@7
	int v6; // ebp@7
	int v7; // esi@7
	unsigned int v8; // ecx@7
	int v9; // ecx@9
	int v10; // ecx@13
	int v11; // edx@13
	int v12; // ebp@13
	int v13; // esi@13
	unsigned int v14; // ecx@13
	int v15; // ecx@15
	int v16; // ecx@19
	int v17; // edx@19
	int v18; // esi@19
	unsigned int v19; // ecx@19
	int v20; // ecx@21
	int v21; // ebp@22
	int v22; // ecx@22
	int v23; // edx@22
	char v24; // si@22
	int v25; // esi@22
	unsigned int v26; // ecx@22
	int v27; // ecx@24
	int v28; // ebp@26
	int v29; // ecx@26
	int v30; // edx@26
	char v31; // si@26
	int v32; // esi@26
	unsigned int v33; // ecx@26
	signed int v34; // [sp+10h] [bp-4h]@1

	v1 = (int)((char *)key + 836);
	v34 = 1024;
	do
	{
		v2 = *((unsigned int *)key + 6) + *((unsigned int *)key + 75) + *((unsigned int *)key + 144);
		if (!v2 || v2 == 3)
		{
			v21 = *((unsigned int *)key + 4);
			v22 = ((unsigned __int8)*((unsigned int *)key + 5) + 1) & 0x3F;
			LowByte(&v23, ((*((unsigned int *)key + 5) + 1) & 0x3F) - *((unsigned int *)key + 3));
			v24 = (*((unsigned int *)key + 5) + 1) & 0x3F;
			*((unsigned int *)key + 5) = v22;
			v23 &= 0x3Fu;
			v25 = (v24 - (BYTE)v21) & 0x3F;
			*((unsigned int *)key + v22 + 7) = *((unsigned int *)key + v25 + 7) + *((unsigned int *)key + v23 + 7);
			v26 = *((unsigned int *)key + *((unsigned int *)key + 5) + 7);
			v27 = v26 < *((unsigned int *)key + v23 + 7) || v26 < *((unsigned int *)key + v25 + 7);
			*((unsigned int *)key + 6) = v27;
			v28 = *((unsigned int *)key + 73);
			v29 = ((unsigned __int8)*((unsigned int *)key + 74) + 1) & 0x3F;
			LowByte(&v30, ((*((unsigned int *)key + 74) + 1) & 0x3F) - *((unsigned int *)key + 72));
			v31 = (*((unsigned int *)key + 74) + 1) & 0x3F;
			*((unsigned int *)key + 74) = v29;
			v30 &= 0x3Fu;
			v32 = (v31 - (BYTE)v28) & 0x3F;
			*((unsigned int *)key + v29 + 76) = *((unsigned int *)key + v32 + 76) + *((unsigned int *)key + v30 + 76);
			v33 = *((unsigned int *)key + *((unsigned int *)key + 74) + 76);
			*((unsigned int *)key + 75) = v33 < *((unsigned int *)key + v30 + 76) || v33 < *((unsigned int *)key + v32 + 76);
		LABEL_19:
			v16 = ((unsigned __int8)*((unsigned int *)key + 143) + 1) & 0x3F;
			*((unsigned int *)key + 143) = v16;
			v17 = ((BYTE)v16 - (unsigned __int8)*((unsigned int *)key + 141)) & 0x3F;
			v18 = ((BYTE)v16 - (unsigned __int8)*((unsigned int *)key + 142)) & 0x3F;
			*((unsigned int *)key + v16 + 145) = *((unsigned int *)key + v18 + 145) + *((unsigned int *)key + v17 + 145);
			v19 = *((unsigned int *)key + *((unsigned int *)key + 143) + 145);
			v20 = v19 < *((unsigned int *)key + v17 + 145) || v19 < *((unsigned int *)key + v18 + 145);
			*((unsigned int *)key + 144) = v20;
			goto LABEL_32;
		}
		v3 = 0;
		if (v2 == 2)
			v3 = 1;
		if (*((unsigned int *)key + 6) == v3)
		{
			v4 = ((unsigned __int8)*((unsigned int *)key + 5) + 1) & 0x3F;
			LowByte(&v5, ((*((unsigned int *)key + 5) + 1) & 0x3F) - *((unsigned int *)key + 3));
			v6 = *((unsigned int *)key + 4);
			*((unsigned int *)key + 5) = v4;
			v5 &= 0x3Fu;
			v7 = ((BYTE)v4 - (BYTE)v6) & 0x3F;
			*((unsigned int *)key + v4 + 7) = *((unsigned int *)key + v7 + 7) + *((unsigned int *)key + v5 + 7);
			v8 = *((unsigned int *)key + *((unsigned int *)key + 5) + 7);
			v9 = v8 < *((unsigned int *)key + v5 + 7) || v8 < *((unsigned int *)key + v7 + 7);
			*((unsigned int *)key + 6) = v9;
		}
		if (*((unsigned int *)key + 75) == v3)
		{
			v10 = ((unsigned __int8)*((unsigned int *)key + 74) + 1) & 0x3F;
			LowByte(&v11, ((*((unsigned int *)key + 74) + 1) & 0x3F) - *((unsigned int *)key + 72));
			v12 = *((unsigned int *)key + 73);
			*((unsigned int *)key + 74) = v10;
			v11 &= 0x3Fu;
			v13 = ((BYTE)v10 - (BYTE)v12) & 0x3F;
			*((unsigned int *)key + v10 + 76) = *((unsigned int *)key + v13 + 76) + *((unsigned int *)key + v11 + 76);
			v14 = *((unsigned int *)key + *((unsigned int *)key + 74) + 76);
			v15 = v14 < *((unsigned int *)key + v11 + 76) || v14 < *((unsigned int *)key + v13 + 76);
			*((unsigned int *)key + 75) = v15;
		}
		if (*((unsigned int *)key + 144) == v3)
			goto LABEL_19;
	LABEL_32:
		*(unsigned int *)v1 = *((unsigned int *)key + *((unsigned int *)key + 74) + 76) ^ *((unsigned int *)key + *((unsigned int *)key + 143) + 145) ^ *((unsigned int *)key + *((unsigned int *)key + 5) + 7);
		v1 += 4;
		--v34;
	} while (v34);
	*((unsigned int *)key + 1) = 0;
	return key;
}

int __cdecl EncryptDecrypt(void* key, Byte* data, int length)
{
	int v3; // edi@1
	void *v4; // esi@2
	int v5; // edx@3
	int v6; // ecx@3
	int result; // eax@4
	int v8; // ebx@7
	char *v9; // edx@7
	unsigned int v10; // edi@8
	int v11; // edi@11
	int lengtha; // [sp+10h] [bp+Ch]@7

	v3 = length;
	if (length > 0)
	{
		v4 = data;
		if (data)
		{
			do
			{
				v5 = *((unsigned int *)key + 1);
				v6 = 4096 - v5;
				if (4096 - v5 > 0)
				{
					if (v6 > v3)
						v6 = v3;
					lengtha = v3 - v6;
					v8 = 0;
					v9 = (char *)key + v5 + 836;
					if (v6 - 3 > 0)
					{
						v10 = ((unsigned int)(v6 - 4) >> 2) + 1;
						v8 = 4 * v10;
						do
						{
							*(unsigned int *)v4 ^= *(unsigned int *)v9;
							v4 = (char *)v4 + 4;
							v9 += 4;
							--v10;
						} while (v10);
					}
					if (v8 < v6)
					{
						v11 = v6 - v8;
						do
						{
							*(BYTE *)v4 ^= *v9;
							v4 = (char *)v4 + 1;
							++v9;
							--v11;
						} while (v11);
					}
					v3 = lengtha;
					result = v6 + *((unsigned int *)key + 1);
					*((unsigned int *)key + 1) = result;
				}
				else
				{
					result = (int)InitKey(key);
				}
			} while (v3 > 0);
		}
	}
	return result;
}

int __cdecl GenerateKey(int seed, Byte* realKey)
{
	unsigned __int8 *v2; // ecx@1
	int v3; // edx@1
	signed int v4; // edx@1
	unsigned __int8 *v5; // edx@1
	int v6; // eax@2
	unsigned __int8 *v7; // edi@2
	signed int v8; // ebx@2
	signed int v9; // esi@3
	int result; // eax@6
	signed int seeda; // [sp+14h] [bp+4h]@1

	v2 = realKey;
	*(unsigned int *)realKey = seed ^ 0x5027919;
	*((unsigned int *)realKey + 2) = seed ^ 0x5027919;
	*((unsigned int *)realKey + 2) = ((seed ^ 0x5027919u) >> 1) | (seed ^ 0x5027919 ^ ((seed ^ 0x5027919 ^ 4 * (seed ^ 0x5027919 ^ 4 * (seed ^ 0x5027919 ^ 2 * (seed ^ 0x5027919 ^ 2 * (seed ^ 0x5027919))))) << 25)) & 0x80000000;
	v3 = 2 * *(unsigned int *)v2 ^ (2 * *(unsigned int *)v2 ^ (*(unsigned int *)v2 >> 1)) & 0x55555555;
	*((unsigned int *)realKey + 71) = v3;
	*((unsigned int *)realKey + 3) = 55;
	*((unsigned int *)realKey + 4) = 24;
	*((unsigned int *)realKey + 71) = ((unsigned int)v3 >> 1) | (v3 ^ ((v3 ^ 4 * (v3 ^ 4 * (v3 ^ 2 * (v3 ^ 2 * v3)))) << 25)) & 0x80000000;
	v4 = ~(16 * *(unsigned int *)v2 ^ (16 * *(unsigned int *)realKey ^ (*(unsigned int *)v2 >> 4)) & 0xF0F0F0F);
	*((unsigned int *)realKey + 140) = v4;
	*((unsigned int *)realKey + 72) = 57;
	*((unsigned int *)realKey + 73) = 7;
	*((unsigned int *)realKey + 140) = ((unsigned int)v4 >> 1) | (v4 ^ ((v4 ^ 4 * (v4 ^ 4 * (v4 ^ 2 * (v4 ^ 2 * v4)))) << 25)) & 0x80000000;
	*((unsigned int *)realKey + 141) = 58;
	*((unsigned int *)realKey + 142) = 19;
	v5 = realKey + 28;
	seeda = 3;
	do
	{
		v6 = *((unsigned int *)v5 - 5);
		v7 = v5;
		v8 = 64;
		do
		{
			v9 = 32;
			do
			{
				v6 = (v6 ^ ((v6 ^ 4 * (v6 ^ 4 * (v6 ^ 2 * (v6 ^ 2 * v6)))) << 25)) & 0x80000000 | ((unsigned int)v6 >> 1);
				--v9;
			} while (v9);
			*(unsigned int *)v7 = v6;
			v7 += 4;
			--v8;
		} while (v8);
		*((unsigned int *)v5 - 1) = 0;
		*((unsigned int *)v5 - 2) = 63;
		v5 += 276;
		result = seeda - 1;
		seeda = result;
	} while (result);
	*((unsigned int *)realKey + 1) = 4096;
	return result;
}

namespace FFEncryptionLibrary
{
	public ref class KeyPair
	{
	private:
		Byte* encryptionKey;
		Byte* decryptionKey;

	public:
		initonly int Seed;

		KeyPair(int seed)
		{
			Seed = seed;

			encryptionKey = new BYTE[0xFFFF];
			decryptionKey = new BYTE[0xFFFF];

			GenerateKey(seed, encryptionKey);
			GenerateKey(seed, decryptionKey);
		}

		int Encrypt(array<Byte> ^ %data)
		{
			pin_ptr<unsigned char> pointer = &data[0];

			return EncryptDecrypt(encryptionKey, (Byte*)pointer, data->Length);
		}

		int Encrypt(array<Byte> ^ %data, int offset, int size)
		{
			pin_ptr<unsigned char> pointer = &data[offset];

			return EncryptDecrypt(encryptionKey, (Byte*)pointer, size);
		}

		int Decrypt(array<Byte> ^ %data)
		{
			pin_ptr<unsigned char> pointer = &data[0];

			return EncryptDecrypt(decryptionKey, (Byte*)pointer, data->Length);
		}

		int Decrypt(array<Byte> ^ %data, int offset, int size)
		{
			pin_ptr<unsigned char> pointer = &data[offset];

			return EncryptDecrypt(decryptionKey, (Byte*)pointer, size);
		}

		~KeyPair()
		{
			delete[] encryptionKey;
			delete[] decryptionKey;
		}
	};
}
