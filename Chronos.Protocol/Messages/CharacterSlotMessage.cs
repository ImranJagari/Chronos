using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Core.IO;
using static Chronos.Protocol.MessageReceiver;
using Chronos.Protocol.Enums;
using Chronos.Protocol.Types;

namespace Chronos.Protocol.Messages
{
    public class CharacterSlotMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.PLAYERSLOT;

        public CharacterType character;
        public int deletedCharacterCount;
        public override ushort MessageId => (ushort)Header;
        public CharacterSlotMessage() { }
        public CharacterSlotMessage(CharacterType character, int deletedCharacterCount)
        {
            this.character = character;
            this.deletedCharacterCount = deletedCharacterCount;
        }
        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            character.Serialize(writer);
            writer.WriteInt(deletedCharacterCount);
        }
    }
}
/*
  CFFServer.on_player_slot = function(ar)
  local index = LAr:ReadInt(ar) + 1
  CWndSelectCharEx.m_slots[index].m_data = {}
  local player = CWndSelectCharEx.m_slots[index].m_data
  player.player_name = LAr:ReadString(ar)
  player.player_id = LAr:ReadInt(ar)
  player.scene_id = LAr:ReadInt(ar)
  player.sex = LAr:ReadByte(ar)
  player.pox_x = LAr:ReadFloat(ar)
  player.pox_y = LAr:ReadFloat(ar) + 20
  player.pox_z = LAr:ReadFloat(ar)
  player.player_level = LAr:ReadInt(ar)
  player.player_job = LAr:ReadInt(ar)
  player.player_str = LAr:ReadInt(ar)
  player.player_sta = LAr:ReadInt(ar)
  player.player_dex = LAr:ReadInt(ar)
  player.player_int = LAr:ReadInt(ar)
  player.player_spi = LAr:ReadInt(ar)
  player.skinset = 0
  player.hair_mesh = LAr:ReadInt(ar)
  player.hair_color = LAr:ReadDword(ar)
  _debug("[CFFServer.on_player_slot] hair_color =% o", player.hair_color)
  player.head_mesh = LAr:ReadInt(ar)
  player.is_block = LAr:ReadInt(ar)
  player.block_time = LAr:ReadInt(ar)
  player.equip_info = {}
  for i = 0, NEUZ.MAX_HUMAN_PARTS - 1 do
    player.equip_info[i] = equip_info_t.new()
  end
  local parts, item_id, flag, attr = nil, nil, nil, nil
  local count = LAr:ReadInt(ar)
  for i = 1, count do
    parts = LAr:ReadByte(ar)
    item_id = LAr:ReadDword(ar)
    flag = LAr:ReadDword(ar)
    attr = LAr:ReadInt(ar)
    player.equip_info[parts].m_itemid = item_id
    player.equip_info[parts].m_word = flag
    player.equip_info[parts].m_option = attr
  end
  for i = 1, 5 do
    item_id = LAr:ReadInt(ar)
    local prop = propItem[item_id]
    if prop then
      parts = prop.Parts
    end
    if parts >= 0 and parts < NEUZ.MAX_HUMAN_PARTS then
      player.equip_info[parts].m_itemid = item_id
      player.equip_info[parts].m_word = 0
      player.equip_info[parts].m_option = 0
    end
  end
  local delete_num = LAr:ReadInt(ar)
  if delete_num > 0 then
    CWndSelectCharEx.m_resume_enable = true
  else
    CWndSelectCharEx.m_resume_enable = false
  end
  if CWndCreateCharEx.this then
    CWndCreateCharEx:on_create_player_success()
  else
    if CWndCreateCharNew.this then
      CWndCreateCharNew:on_create_player_success()
    end
  else
    if CWndSelectCharEx.this then
      CWndSelectCharEx:update_active(index)
      CWndSelectCharEx:refresh()
    end
  end
end
*/