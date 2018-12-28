using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class FlagsDataType //Finish flags serialize
    {
        public const int End = 7621499;
        public byte m_need_activate_game;
        public byte m_game_flag;
        public int m_last_logout_time;
        public int pet_id;
        public int title_list_count;
        public int[] titles;
        public int title_renown_list_count;
        public int[] titles_renown;
        public byte renown_title_host_count;
        public int[] title_id_renown_host;
        public int[] host_ids;
        public InventoryType inventory;
        public QuestInventoryType questInventory;
        public int bank_capacity;
        public TaskbarType taskbar;
        public int skillsCount;
        public SkillType[] skills;
        public uint domesticate_skill_time;
        public bool questChangeflag;
        public FriendListType friendList;
        public FactionType faction;
        public int pos_count;
        public MemorisedPositionType[] memorisedPositions;
        public int duel_win;
        public int duel_lost;
        public int duel_total;
        public int pk_win;
        public int pk_total;
        public int adv_stamina;
        public byte auto_assign;
        public CreditCardType creditCard;
        public byte vip_level_game;
        public byte vip_level_gm;
        public int vip_expired_date;
        public int last_wage_time_game;
        public int last_wage_time_gm;
        public byte stone_attr_str;
        public byte stone_attr_dex;
        public byte stone_attr_sta;
        public byte stone_attr_spi;
        public byte stone_attr_int;
        public int lover_count;
        public LoverType[] lovers;
        public int closet_capacity;
        public int closet_level;
        public int fate_count;
        public FateClosetType[] closets;
        public VesselType vessel;
        public HotkeyType hotkey;
        public SpiritTatooType spiritTatoo;
        public AdventType advent;
        public PetDomesticateType pet;
        public ExpeditionType expedition;
        public int cbg_sign_time;
        public uint cbg_sign_player;
        public uint cbg_sell_player;
        public FlagsDataType()
        {

        }
        public FlagsDataType(byte m_need_activate_game, byte m_game_flag, int m_last_logout_time, int pet_id, int title_list_count, int[] titles,
            int title_renown_list_count, int[] titles_renown, byte renown_title_host_count, int[] title_id_renown_host, int[] host_ids,
            InventoryType inventory, QuestInventoryType questInventory, int bank_capacity, TaskbarType taskbar, int skillsCount, SkillType[] skills,
            uint domesticate_skill_time, bool questChangeflag, FriendListType friendList, FactionType faction, int pos_count, MemorisedPositionType[] memorisedPositions,
            int duel_win, int duel_lost, int duel_total, int pk_win, int pk_total, int adv_stamina, byte auto_assign, CreditCardType creditCard, byte vip_level_game,
            byte vip_level_gm, int vip_expired_date,int last_wage_time_game, int last_wage_time_gm, byte stone_attr_str, byte stone_attr_dex, byte stone_attr_sta,
            byte stone_attr_spi, byte stone_attr_int, int lover_count, LoverType[] lovers, int closet_capacity, int closet_level, int fate_count,
            FateClosetType[] closets, VesselType vessel, HotkeyType hotkey, SpiritTatooType spiritTatoo, AdventType advent, PetDomesticateType pet, ExpeditionType expedition,
            int cbg_sign_time, uint cbg_sign_player, uint cbg_sell_player)
        {
            this.m_need_activate_game = m_need_activate_game;
            this.m_game_flag = m_game_flag;
            this.m_last_logout_time = m_last_logout_time;
            this.pet_id = pet_id;
            this.title_list_count = title_list_count;
            this.titles = titles;
            this.title_renown_list_count = title_renown_list_count;
            this.titles_renown = titles_renown;
            this.renown_title_host_count = renown_title_host_count;
            this.title_id_renown_host = title_id_renown_host;
            this.host_ids = host_ids;
            this.inventory = inventory;
            this.questInventory = questInventory;
            this.bank_capacity = bank_capacity;
            this.taskbar = taskbar;
            this.skillsCount = skillsCount;
            this.skills = skills;
            this.domesticate_skill_time = domesticate_skill_time;
            this.questChangeflag = questChangeflag;
            this.friendList = friendList;
            this.faction = faction;
            this.pos_count = pos_count;
            this.memorisedPositions = memorisedPositions;
            this.duel_win = duel_win;
            this.duel_lost = duel_lost;
            this.duel_total = duel_total;
            this.pk_win = pk_win;
            this.pk_total = pk_total;
            this.adv_stamina = adv_stamina;
            this.auto_assign = auto_assign;
            this.creditCard = creditCard;
            this.vip_level_game = vip_level_game;
            this.vip_level_gm = vip_level_gm;
            this.vip_expired_date = vip_expired_date;
            this.last_wage_time_game = last_wage_time_game;
            this.last_wage_time_gm = last_wage_time_gm;
            this.stone_attr_str = stone_attr_str;
            this.stone_attr_dex = stone_attr_dex;
            this.stone_attr_sta = stone_attr_sta;
            this.stone_attr_spi = stone_attr_spi;
            this.stone_attr_int = stone_attr_int;
            this.lover_count = lover_count;
            this.lovers = lovers;
            this.closet_capacity = closet_capacity;
            this.closet_level = closet_level;
            this.fate_count = fate_count;
            this.closets = closets;
            this.vessel = vessel;
            this.hotkey = hotkey;
            this.spiritTatoo = spiritTatoo;
            this.advent = advent;
            this.pet = pet;
            this.expedition = expedition;
            this.cbg_sign_time = cbg_sign_time;
            this.cbg_sign_player = cbg_sign_player;
            this.cbg_sell_player = cbg_sell_player;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)PlayerFlagEnum.flag_base);
            writer.WriteChar((char)m_need_activate_game);
            writer.WriteChar((char)m_game_flag);
            writer.WriteInt(m_last_logout_time);
            writer.WriteUInt((uint)pet_id);

            writer.WriteInt((int)title_list_count);
            foreach (int title in titles)
                writer.WriteInt(title);

            writer.WriteInt(title_renown_list_count);
            foreach (int title in titles_renown)
                writer.WriteInt(title);

            writer.WriteByte(renown_title_host_count);
            for(byte i = 0; i < renown_title_host_count; i++)
            {
                writer.WriteInt(title_id_renown_host[i]);
                writer.WriteInt(host_ids[i]);
            }
            inventory.Serialize(writer);

            writer.WriteInt((int)PlayerFlagEnum.flag_inventory);
            questInventory.Serialize(writer);

            writer.WriteInt((int)PlayerFlagEnum.flag_tbag);
            writer.WriteInt(bank_capacity);
            taskbar.Serialize(writer);

            writer.WriteInt((int)PlayerFlagEnum.flag_taskbar);
            writer.WriteUInt((uint)skillsCount);
            foreach (SkillType skill in skills)
                skill.Serialize(writer);
            writer.WriteUInt(domesticate_skill_time);

            writer.WriteInt((int)PlayerFlagEnum.flag_skill);
            writer.WriteByte(1);//Setted to false by default to avoid to do this system now -> variable => questChangeflag
            //add here the questchangeSerialize
            writer.WriteUInt(0);
            writer.WriteUInt(0);
            writer.WriteShort(0);
            writer.WriteInt((int)PlayerFlagEnum.flag_quest);
            friendList.Serialize(writer);

            writer.WriteInt((int)PlayerFlagEnum.flag_messenger);
            faction.Serialize(writer);

            writer.WriteInt((int)PlayerFlagEnum.flag_faction);
            writer.WriteInt(pos_count);
            foreach (MemorisedPositionType position in memorisedPositions)
                position.Serialize(writer);
            
            writer.WriteInt(duel_win);
            writer.WriteInt(duel_lost);
            writer.WriteInt(duel_total);
            writer.WriteInt(pk_win);
            writer.WriteInt(pk_total);
            writer.WriteInt(adv_stamina);
            writer.WriteByte(auto_assign);

            creditCard.Serialize(writer);

            writer.WriteInt((int)PlayerFlagEnum.flag_credit);
            writer.WriteByte(vip_level_game);
            writer.WriteByte(vip_level_gm);
            writer.WriteInt(vip_expired_date);
            writer.WriteInt(last_wage_time_game);
            writer.WriteInt(last_wage_time_gm);
            writer.WriteByte(stone_attr_str);
            writer.WriteByte(stone_attr_dex);
            writer.WriteByte(stone_attr_sta);
            writer.WriteByte(stone_attr_spi);
            writer.WriteByte(stone_attr_int);
            writer.WriteInt(lover_count);
            foreach (LoverType lover in lovers)
                lover.Serialize(writer);

            writer.WriteInt((int)PlayerFlagEnum.flag_lover);
            writer.WriteInt(closet_capacity);
            writer.WriteInt(closet_level);
            writer.WriteInt(fate_count);
            foreach (FateClosetType closet in closets)
                closet.Serialize(writer);

            writer.WriteInt((int)PlayerFlagEnum.flag_closet);
            vessel.Serialize(writer);

            writer.WriteInt((int)PlayerFlagEnum.flag_vessel);
            hotkey.Serialize(writer);

            writer.WriteInt((int)PlayerFlagEnum.flag_hotkey);
            spiritTatoo.Serialize(writer);

            writer.WriteInt((int)PlayerFlagEnum.flag_magic_t);
            advent.Serialize(writer);

            writer.WriteInt((int)PlayerFlagEnum.flag_advent);
            pet.Serialize(writer);

            writer.WriteInt((int)PlayerFlagEnum.flag_domesticate);
            expedition.Serialize(writer);

            writer.WriteInt((int)PlayerFlagEnum.flag_expedition);
            writer.WriteInt(cbg_sign_time);
            writer.WriteUInt(cbg_sign_player);
            writer.WriteUInt(cbg_sell_player);
            writer.WriteInt(End);
        }
    }
}
/*system to do later
    if quest_change_flag == 1 then
      self.m_quest_list = {}
      self.m_complete_quest = {}
      self.m_repeat_quest = {}
      self.m_secret_items = {}
      self.m_achieves = {}
      local nSize = LAr:ReadDword(ar)
      for i = 1, nSize do
        local quest = quest_t:new()
        quest:serialize(ar)
        self.m_quest_list[quest.id] = quest
      end
      nSize = LAr:ReadDword(ar)
      for i = 1, nSize do
        self.m_complete_quest[LAr:ReadDword(ar)] = true
      end
      nSize = LAr:ReadWord(ar)
      for i = 1, nSize do
        self.m_repeat_quest[LAr:ReadDword(ar)] = LAr:ReadWord(ar)
      end
    end
*/