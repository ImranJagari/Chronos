using Chronos.Core.Utils;
using Chronos.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorNewGen
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleUtils.ChangeTitle("Chronos 1.0.0");

            SimpleServer.DrawAscii();
            SimpleServer server = new SimpleServer();

            Console.WriteLine(" ");
            ConsoleUtils.WriteMessageInfo("Initializing all stuffs !");


            server.Initialize();

            server.Start(SimpleServer.Host, SimpleServer.Port);

            ConsoleUtils.WriteMessageInfo("Server Started !");
            ConsoleUtils.WriteMessageInfo($"Listening on ip {SimpleServer.Host} and on port {SimpleServer.Port} !");

            Console.ReadLine();
        }
    }
}
