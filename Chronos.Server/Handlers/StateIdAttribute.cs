using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Handlers
{
    public class StateIdAttribute : Attribute
    {
        public StateTypeEnum MessageId
        {
            get;
            set;
        }
        public StateIdAttribute(StateTypeEnum messageId)
        {
            MessageId = messageId;
        }
    }
}
