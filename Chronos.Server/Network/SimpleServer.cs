using Chronos.ORM;
using Chronos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Chronos.Core.Utils;
using Chronos.Server.Initialization;
using Chronos.Core.Reflection;
using Chronos.Server.Manager;
using Chronos.Protocol;
using Chronos.Core.Attributes;
using System.IO;
using Chronos.Core.Xml.Config;

namespace Chronos.Server.Network
{
    public class SimpleServer : Singleton<SimpleServer>
    {

        #region Variables

        private Socket socketListener;
        private bool runing = false;
        private const string configFilePath = ".//config.xml";

        [Variable]
        public static string Host = "127.0.0.1";

        [Variable]
        public static short Port = 443;

        [Variable]
        public static DatabaseConfiguration DatabaseConfiguration = new DatabaseConfiguration
        {
            Host = "localhost",
            DbName = "chronos",
            User = "root",
            Password = "",
            ProviderName = "MySql.Data.MySqlClient"
        };

        public DatabaseAccessor DBAccessor
        {
            get;
            protected set;
        }
        public InitializationManager InitManager
        {
            get;
            set;
        }
        public XmlConfig Config
        {
            get;
            protected set;
        }

        public static List<SimpleClient> ConnectedClients;


        public SimpleServer()
        {
            ConnectedClients = new List<SimpleClient>();
            socketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        #endregion

        #region Methods
        public static void DrawAscii()
        {
            string[] Logo = {
            "   ____ _                                   ",
            "  / ___| |__  _ __ ___  _ __   ___  ___     ",
            " | |   | '_ \\| '__/ _ \\| '_ \\ / _ \\/ __|",
            " | |___| | | | | | (_) | | | | (_) \\__ \\  ",
            "  \\____|_| |_|_|  \\___/|_| |_|\\___/|___/ "
            };
            foreach (var lines in Logo)
            {
                Console.WriteLine(lines);
            }
        }
        public void Start(string ip, short listenPort)
        {
            runing = true;
            socketListener.Bind(new IPEndPoint(IPAddress.Parse(ip), listenPort));
            socketListener.Listen(5);
            socketListener.BeginAccept(BeginAcceptCallBack, socketListener);
        }

        public void Stop()
        {
            runing = false;
            socketListener.Shutdown(SocketShutdown.Both);
        }
        public void Initialize()
        {
            ConsoleUtils.WriteMessageInfo("Initializing Configuration !");

            Config = new XmlConfig(configFilePath);
            Config.AddAssemblies(AppDomain.CurrentDomain.GetAssemblies().ToDictionary(entry => entry.GetName().Name).Values.ToArray());

            if (!File.Exists(configFilePath))
            {
                Config.Create();
                ConsoleUtils.WriteMessageInfo("Config file created !");
            }
            else
                Config.Load();


            ConsoleUtils.WriteMessageInfo("Loading protocol messages !");
            MessageReceiver.Initialize();

            //ConsoleUtils.WriteMessageInfo("Loading handlers !");
            //PacketManager.Initialize(Assembly.GetExecutingAssembly());

            ConsoleUtils.WriteMessageInfo("Loading Database !");
            DBAccessor = new DatabaseAccessor(DatabaseConfiguration);
            DBAccessor.RegisterMappingAssembly(Assembly.GetExecutingAssembly());
            DBAccessor.Initialize();
            DBAccessor.OpenConnection();
            DataManagerAllocator.Assembly = System.Reflection.Assembly.GetExecutingAssembly();
            DatabaseManager.DefaultDatabase = DBAccessor.Database;

            ConsoleUtils.WriteMessageInfo("Loading database features !");
            this.InitManager = Singleton<InitializationManager>.Instance;
            this.InitManager.AddAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            this.InitManager.Initialize(InitializationPass.Database);
            InitManager.InitializeAll();
        }
        private void BeginAcceptCallBack(IAsyncResult result)
        {
            if (runing)
            {
                Socket listener = (Socket)result.AsyncState;
                Socket acceptedSocket = listener.EndAccept(result);

                SimpleClient client = new SimpleClient(acceptedSocket);
                AddClient(client);

                ConsoleUtils.WriteSuccess($"Client <{client.IP}> is connected !");
                OnConnectionAccepted(acceptedSocket);
                socketListener.BeginAccept(BeginAcceptCallBack, socketListener);
            }
        }
        public static void AddClient(SimpleClient client)
        {
            ConnectedClients.Add(client);
        }
        public static void RemoveClient(SimpleClient client)
        {
            ConnectedClients.Remove(client);
        }
        #endregion

        #region Events

        public delegate void ConnectionAcceptedDelegate(Socket acceptedSocket);
        public event ConnectionAcceptedDelegate ConnectionAccepted;
        private void OnConnectionAccepted(Socket client)
        {
            if (ConnectionAccepted != null)
                ConnectionAccepted(client);
        }

        #endregion

    }
}
