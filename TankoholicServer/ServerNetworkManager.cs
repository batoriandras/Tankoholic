using Riptide;
using Riptide.Utils;
using System;

namespace TankoholicServer
{


    public class ServerNetworkManager
    {
        private static ServerNetworkManager? instance;
        public static ServerNetworkManager Instance
        {
            get
            {
                instance ??= new ServerNetworkManager();
                
                return instance;
            }
        }

        private ServerNetworkManager() { }

        public Riptide.Server Server { get; private set; }

        private ushort port = 7070;
        private ushort maxClient = 2;

        public void Initialize()
        {

            RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.WriteLine, false);

            Server = new Riptide.Server();
            Server.Start(port, maxClient);

            Server.ClientDisconnected += PlayerLeft;
        }

        public void Update()
        {
            Server.Update();
        }

        public void Stop()
        {
            Server.Stop();
        }

        public void PlayerLeft(object sender, ServerDisconnectedEventArgs e)
        {
            
        }
    }
}
