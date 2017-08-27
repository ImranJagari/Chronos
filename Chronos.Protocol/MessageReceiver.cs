using Chronos.Core.IO;
using Chronos.Core.Reflection;
using Chronos.Protocol.Enums;
using Chronos.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol
{
    public class MessageReceiver
    {
        private static readonly Dictionary<HeaderEnum, Type> Messages = new Dictionary<HeaderEnum, Type>();
        private static readonly Dictionary<HeaderEnum, Func<NetworkMessage>> Constructors = new Dictionary<HeaderEnum, Func<NetworkMessage>>();

        public static void Initialize()
        {
            var assembly = Assembly.GetAssembly(typeof(MessageReceiver));
            foreach (var type in assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(NetworkMessage))))
            {
                var field = type.GetField("Header");
                if (field != null)
                {
                    var num = (HeaderEnum)field.GetValue(type);
                    if (Messages.ContainsKey(num))
                    {
                        throw new AmbiguousMatchException(
                            $"MessageReceiver() => {num} item is already in the dictionary, old type is : {Messages[num]}, new type is  {type}");
                    }
                    Messages.Add(num, type);
                    var constructor = type.GetConstructor(Type.EmptyTypes);
                    if (constructor == null)
                    {
                        throw new Exception($"'{type}' doesn't implemented a parameterless constructor");
                    }
                    Constructors.Add(num, constructor.CreateDelegate<Func<NetworkMessage>>());
                }
            }
        }

        public static NetworkMessage BuildMessage(HeaderEnum id, IDataReader reader)
        {
            if (!Messages.ContainsKey(id))
            {
                return null;
            }
            var message = Constructors[id]();
            if (message == null)
            {
                return null;
            }
            message.Unpack(reader);
            return message;
        }

        public static Type GetMessageType(HeaderEnum id)
        {
            if (!Messages.ContainsKey(id))
            {
                return null;
            }
            return Messages[id];
        }

        [Serializable]
        public class MessageNotFoundException : Exception
        {
            public MessageNotFoundException()
            {
            }

            public MessageNotFoundException(string message)
                : base(message)
            {
            }

            public MessageNotFoundException(string message, Exception inner)
                : base(message, inner)
            {
            }

            protected MessageNotFoundException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        public class NetworkMessageException : Exception
        {
            public static Exception DeserializeException => new Exception("Packet non deserializable, it's a packet sended by the server !");
            public static Exception SerializeException => new Exception("Packet non serializable, it's a packet sended by the client !");
        }
    }
}