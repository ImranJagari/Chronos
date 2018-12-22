using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using static Chronos.Protocol.MessageReceiver;
using Chronos.Protocol.Types;

namespace Chronos.Protocol.Messages
{
    public class CharactersListMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.PLAYERLIST;
        public int server_time;
        public byte aggrementAccepted;
        public byte characters_count;
        public uint m_cbg_sell_player;
        public int cbg_ordersn;
        public uint m_cbg_sign_player;
        public int m_cbg_sign_time;
        public CharacterType[] characters;
        public int deletedCharactersCount;
        public int cityId;
        public int provinceId;
        public sbyte m_realName; //put to 7 to empty slots ????
        public override ushort MessageId => (ushort)Header;

        public CharactersListMessage() { }

        public CharactersListMessage(int server_time, byte aggrementAccepted, byte characters_count, uint m_cbg_sell_player, int cbg_ordersn, uint m_cbg_sign_player, int m_cbg_sign_time, CharacterType[] characters, int deletedCharactersCount, int cityId, int provinceId, sbyte m_realName)
        {
            this.server_time = server_time;
            this.aggrementAccepted = aggrementAccepted;
            this.characters_count = characters_count;
            this.m_cbg_sell_player = m_cbg_sell_player;
            this.cbg_ordersn = cbg_ordersn;
            this.m_cbg_sign_player = m_cbg_sign_player;
            this.m_cbg_sign_time = m_cbg_sign_time;
            this.characters = characters;
            this.deletedCharactersCount = deletedCharactersCount;
            this.cityId = cityId;
            this.provinceId = provinceId;
            this.m_realName = m_realName;
        }

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(server_time);
            writer.WriteByte(aggrementAccepted);
            writer.WriteByte(characters_count);
            writer.WriteUInt(m_cbg_sell_player);
            writer.WriteInt(cbg_ordersn);
            writer.WriteUInt(m_cbg_sign_player);
            writer.WriteInt(m_cbg_sign_time);
            for (int i = 0; i < characters_count; i++)
            {
                characters[i].Serialize(writer);
            }
            writer.WriteInt(deletedCharactersCount);
            writer.WriteInt(cityId);
            writer.WriteInt(provinceId);
            writer.WriteSByte(m_realName);
        }
    }

    /*
        CWndSelectCharEx.update_player_data = function(self, ar)
      local server_time = LAr:ReadInt(ar)
      self.m_new_flag = LAr:ReadByte(ar)
      local player_count = LAr:ReadByte(ar)
      self.m_cbg_sell_player = LAr:ReadDword(ar)
      local cbg_ordersn = LAr:ReadInt(ar)
      self.m_slots = {}
    self.m_server_time = server_time
    local tm = os.date("! * T", server_time)
      _debug("[CFFServer.on_player_list] server_time =", server_time, "tm.year =", tm.year, "tm.month =", tm.month, "tm.day =", tm.day)

      local delete_num = LAr:ReadInt(ar)
      self.m_city_id = LAr:ReadInt(ar)
      self.m_province_id = LAr:ReadInt(ar)
      self.m_realname = LAr:ReadChar(ar)
      _debug(string.format("[On_player_list] delete_num =%d, realname =%d", delete_num, self.m_realname))
      if delete_num > 0 then
        self.m_resume_enable = true
      else
        self.m_resume_enable = false
      end
    end




    realname ??????
        if self.m_realname == 7 or skip == true then
          this:Destroy()
          CWndCreateCharEx.m_slot = self.m_empty_slot
          CWndCreateCharNew.m_slot = self.m_empty_slot
          CWndMgr:open_create_char_wnd()
        else
 */
}
