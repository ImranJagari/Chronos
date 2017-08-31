using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using static Chronos.Protocol.MessageReceiver;
using Chronos.Protocol.Types;

namespace Chronos.Protocol.Messages
{
    public class CreateCharacterMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.CREATEPLAYER;

        public string name;
        public string hd_md5;
        public int slot;
        public byte job;
        public byte sex;
        public byte hair_mesh;
        public uint hair_color;
        public byte head_mesh;
        public int city_code;
        public int constellation;
        public byte country;
        public string sn_card;
        public int card_type;
        public string hd_sn;
        public string bin_account;
        public ClosetItemType[] closets;

        public override ushort MessageId => (ushort)Header;
        public CreateCharacterMessage() { }
        public override void Deserialize(IDataReader reader)
        {
            name = reader.ReadUTF();
            hd_md5 = reader.ReadUTF();
            slot = reader.ReadInt();
            job = reader.ReadByte();
            sex = reader.ReadByte();
            hair_mesh = reader.ReadByte();
            hair_color = reader.ReadUInt();
            head_mesh = reader.ReadByte();
            city_code = reader.ReadInt();
            constellation = reader.ReadInt();
            country = reader.ReadByte();
            sn_card = reader.ReadUTF();
            card_type = reader.ReadInt();
            hd_sn = reader.ReadUTF();
            bin_account = reader.ReadUTF();
            closets = new ClosetItemType[5];
            for(int i = 0; i < 5; i++)
            {
                closets[i] = new ClosetItemType();
                closets[i].Deserialize(reader);
            }
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.DeserializeException;
        }
    }
}

/*
  before_send(ar, PACKETTYPE.CREATEPLAYER)
  ar:WriteString(name)
  ar:WriteString(hd_md5)
  ar:WriteInt(slot)
  ar:WriteByte(job)
  ar:WriteByte(sex)
  ar:WriteByte(hair_mesh)
  ar:WriteDword(hair_color)
  ar:WriteByte(head_mesh)
  ar:WriteInt(city_code)
  ar:WriteInt(constellation)
  ar:WriteByte(country)
  ar:WriteString(sn_card)
  ar:WriteInt(card_type)
  ar:WriteString(hd_sn)
  ar:WriteString(bin_account)
  for i = 1, 5 do
    ar:WriteInt(sel_cloth_list[i] or 0)
  end
*/