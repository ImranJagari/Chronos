using Chronos.Protocol.Messages;
using Chronos.Server.Game.Actors.Context.Characters;
using Chronos.Server.Network;
using System.Linq;

namespace Chronos.Server.Handlers.Characters
{
    public partial class CharacterHandler
    {
        public static void SendCharactersListMessage(IPacketInterceptor client, int server_time, byte aggrementAccepted, byte characters_count, uint m_cbg_sell_player,
            int cbg_ordersn, uint m_cbg_sign_player, int m_sign_time, Character[] characters, int deletedCharactersCount, int cityId, int provinceId, sbyte m_realName)
        {
            client.Send(new CharactersListMessage(server_time, aggrementAccepted, characters_count, m_cbg_sell_player, cbg_ordersn, m_cbg_sign_player, m_sign_time, characters.Select(x => x.GetNetwork()).ToArray(), deletedCharactersCount, cityId, provinceId, m_realName));
        }
        public static void SendCharacterSlotMessage(IPacketInterceptor client, Character character, int deletedCharactersCount)
        {
            client.Send(new CharacterSlotMessage(character.GetNetwork(), deletedCharactersCount));
        }
    }
}
