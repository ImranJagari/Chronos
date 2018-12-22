using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class FriendListType
    {
        FriendStateEnum my_state;
        int favor_value;
        int flower_count;
        int[] flowers_ids;
        int friend_count;
        FriendMemberType[] friends;
        int blacklisted_friend_count;
        BlacklistedFriendMemberType[] blacklist;
        int murdered_friend_count;
        FriendMemberType[] murderedlist;

        public FriendListType(FriendStateEnum my_state, int favor_value, int flower_count, int[] flowers_ids, int friend_count, FriendMemberType[] friends,
            int blacklisted_friend_count, BlacklistedFriendMemberType[] blacklist, int murdered_friend_count, FriendMemberType[] murderedlist)
        {
            this.my_state = my_state;
            this.favor_value = favor_value;
            this.flower_count = flower_count;
            this.flowers_ids = flowers_ids;
            this.friend_count = friend_count;
            this.friends = friends;
            this.blacklisted_friend_count = blacklisted_friend_count;
            this.blacklist = blacklist;
            this.murdered_friend_count = murdered_friend_count;
            this.murderedlist = murderedlist;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)my_state);
            writer.WriteInt(favor_value);
            writer.WriteInt(flower_count);
            foreach (int id in flowers_ids)
                writer.WriteInt(id);
            writer.WriteInt(friend_count);
            foreach (FriendMemberType member in friends)
                member.Serialize(writer);
            writer.WriteInt(blacklisted_friend_count);
            foreach (BlacklistedFriendMemberType blacklisted in blacklist)
                blacklisted.Serialize(writer);
            writer.WriteInt(murdered_friend_count);
            foreach (FriendMemberType murdered in murderedlist)
                murdered.Serialize(writer);
        }
    }
}
