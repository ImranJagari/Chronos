using Chronos.Core.Extensions;
using Chronos.Protocol;
using Chronos.Protocol.Enums;
using Chronos.Protocol.Messages;
using Chronos.Protocol.Messages.Snapshots;
using Chronos.Protocol.Types;
using Chronos.Protocol.Types.ObjectsType;
using Chronos.Server.Game.Actors.Context.Characters;
using Chronos.Server.Game.Stats;
using Chronos.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Chronos.Server.Handlers.Roleplay
{
    public partial class ContextRoleplayHandler
    {
        [HeaderPacket(HeaderEnum.JOIN)]
        public static void HandleJoinMessage(SimpleClient client, JoinMessage message)
        {
            Character character = client.Account.Characters.FirstOrDefault(x => x.Id == message.characterId);
            if(character == null)
            {
                client.Disconnect();
                return;
            }
            client.Character = character;

            SendJoinRightMessage(client, message.characterId, character.Position.Map.SceneId, character.Position.X, character.Position.Y, character.Position.Z, false, 0);//Send all players in the map


            SendSnapshotMessage(client, new Snapshot[] { new SetStateVerSnapshot(1) });
            SendSnapshotMessage(client, new Snapshot[] { new UpdateServerTimeSnapshot(DateTime.Now.GetUnixTimeStamp()) });
            //Thread.Sleep(5000);

            SendSnapshotMessage(client, new Snapshot[] { new AddObjectSnapshot(new CharacterObjectType(ObjectTypeEnum.OT_PLAYER, (uint)client.Character.GetHashCode(), 12, (uint)0xFFFFFFFF,
                client.Character.Position.X, client.Character.Position.Y, client.Character.Position.Z, 0, 0, 100, client.Character.Name, client.Character.Stats.Fields.Count,
                client.Character.Stats.Fields.Keys.Select(x => (ushort)x).ToArray(), client.Character.Stats.Fields.Values.Select(x => x.Total).ToArray(),
                (byte)0, new byte[0], new int[0], new int[0], (uint)client.Character.Id, client.Character.Sex ? (byte)1 : (byte)0, client.Character.Job, (int)client.Account.Authority,
                SimpleServer.ServerId, "", (short)client.Character.Record.Constellation, (short)0, (byte)client.Character.HairMesh,
                client.Character.HairColor, (byte)client.Character.HeadMesh, (uint)0 /*Todo : option*/, 0, "", 0, 0, 0/*Todo : fame*/,
                0, 0, 0, 0x5322AFDF, 0, 0, new ItemType[0], 0, new byte[0], new int[0], 8, new int[0], 0, 0, new int[0], new int[0],
                new KingdomType((byte)0, (byte)0, 0, $"", (byte)0,0,0,0,0,0,0, $""),
                new MasterType(client.Character.Name, "", "", 0,0,0), new MarriageType(0), 0, new int[0], 0, 0, false, true,
                new FlagsDataType(0,1,139495485,0,0,new int[0],0, new int[0], 0,new int[0], new int[0],
                new InventoryType(new short[]
                                {
                                    0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D,
                                    0x0E, 0x0F, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B,
                                    0x1C, 0x1D, 0x1E, 0x1F, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29
                                }, 0, new ItemElementType[0], new short[0]),
                new QuestInventoryType(new short[]
                                {
                                    0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D,
                                    0x0E, 0x0F, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B,
                                    0x1C, 0x1D, 0x1E, 0x1F, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29
                                }, 0, new ItemElementType[0],new short[0]),0,
                new TaskbarType(0, new ShortcutType[0],0, new ShortcutType[0],0,
                new ShortcutType[0],0, new byte[0], 0), 0 , new SkillType[0],
                0,false,
                new FriendListType(FriendStateEnum.FRS_ONLINE, 0,0,new int[0], 0, new FriendMemberType[0],0, new FriendMemberType[0],0, new FriendMemberType[0]),
                new FactionType(0,0,0,0,0,"", new ProtegeType[0]), 0,
                new MemorisedPositionType[0], 0,0,0,0,0,0,0,
                new CreditCardType(CreditCardTypeEnum.CREDIT_CRAD_NORMAL, 0,0,0,0,0,0,(uint)DateTime.Now.GetUnixTimeStamp(), 0,(uint)DateTime.Now.GetUnixTimeStamp(), 0,0,0),
                0,0,0,0,0,StatsFields.StoneStrenghtBoost, StatsFields.StoneDexterityBoost, StatsFields.StoneStaminaBoost, StatsFields.StoneSPIBoost, StatsFields.StoneIntelligenceBoost,
                0, new LoverType[0], client.Character.Inventory.ClosetItems.Count, 2, client.Character.Inventory.ClosetItems.Count,
                client.Character.Inventory.ClosetItems.Values.Select(x => x.GetFateClosetType()).ToArray(),
                new VesselType(1,0,0,0,0,new int[0], new WingType[0]),new HotkeyType(0,new HotkeyEnum[0], new HotkeyEnum[0]),
                new SpiritTatooType(""), new AdventType(0,0,0,0,""),
                new PetDomesticateType(0,0 , new PetDomesticateStatsType[0]), new ExpeditionType(1,0,0,0,"", 0),0,0,0))) });

            //foreach(var stats in client.Character.Stats.Fields)
            //{
            //    SendSnapshotMessage(client, new Snapshot[] { new SetValueObjectSnapshot((uint)client.Character.GetHashCode(), (short)stats.Key, (int)stats.Value.Total) });
            //}
        }
        public static void SendJoinRightMessage(IPacketInterceptor client, int characterId, int sceneId, float x, float y, float z, bool isFestival, uint festivalDay)
        {
            client.Send(new JoinRightMessage(characterId, sceneId, new PositionType(x, y, z), isFestival, festivalDay));
        }
    }
}