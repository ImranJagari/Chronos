using Chronos.Server.Game.Actors.Characters;
using Chronos.Server.Network;
using Chronos.Protocol.Enums;
using Chronos.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Handlers.Characters
{
    public partial class CharacterHandler
    {
        public void SendCharactersListMessage(IPacketInterceptor client, int server_time, byte aggrementAccepted, byte characters_count, uint m_cbg_sell_player,
            int cbg_ordersn, Character[] characters, int deletedCharactersCount, int cityId, int provinceId, sbyte m_realName)
        {
            client.Send(new CharactersListMessage());
        }
    }
}
