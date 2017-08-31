using Chronos.Core.Reflection;
using Chronos.Server.Network;
using Chronos.Protocol.Enums;
using Chronos.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Handlers
{
    public class PacketManager
    {
        public static Dictionary<HeaderEnum, Action<object, SimpleClient, NetworkMessage>> MethodHandlers = new Dictionary<HeaderEnum, Action<object, SimpleClient, NetworkMessage>>();
        public static void Initialize(Assembly asm)
        {
            var methods = asm.GetTypes()
                      .SelectMany(t => t.GetMethods())
                      .Where(m => m.GetCustomAttributes(typeof(HeaderPacketAttribute), false).Length > 0)
                      .ToArray();
            foreach (var method in methods)
            {
                var action =  DynamicExtension.CreateDelegate(method, typeof(SimpleClient), typeof(NetworkMessage)) as Action<object, SimpleClient, NetworkMessage>;
                MethodHandlers.Add((HeaderEnum)method.CustomAttributes.ToArray()[0].ConstructorArguments[0].Value, action);
            }
        }
        public static void ParseHandler(SimpleClient client, NetworkMessage message)
        {
            try
            {
                Action<object, SimpleClient, NetworkMessage> methodToInvok;
                if (message != null)
                {
                    if (MethodHandlers.TryGetValue((HeaderEnum)message.MessageId, out methodToInvok))
                    {
                        methodToInvok.Invoke(null, client, message);
                    }
                    else
                    {
                        Console.WriteLine(string.Format("Received non handled Packet : id = {0} -> {1}", message.MessageId, message));
                    }
                }
                else
                {
                    Console.WriteLine("Receive empty packet");
                    client.Disconnect();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
