using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Handlers
{
    public class HeaderPacketAttribute : Attribute
    {
        public HeaderEnum MessageId
        {
            get;
            set;
        }
        public HeaderPacketAttribute(HeaderEnum messageId)
        {
            MessageId = messageId;
        }
    }
}
