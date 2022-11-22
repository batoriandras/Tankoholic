using System;
using Riptide;
using Riptide.Utils;
using TankoholicClassLibrary;

namespace TankoholicClient
{
    public class ClientNetworkManager
    {
        private static ClientNetworkManager instance;
        public static ClientNetworkManager Instance
        {
            get
            {
                instance ??= new ClientNetworkManager();
                return instance;
            }
        }

        public Client Client { get; private set; }

        private string ip = "127.0.0.1";
        private ushort port = 7070;


        public void Initialize()
        {
            RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.WriteLine, false);

            Client = new Client();
            Client.ClientDisconnected += (s, e) => Player.OtherPlayers.Remove(e.Id);
        }

        public void Connect()
        {
            Client.Connect($"{ip}:{port}");
        }

        public void Update()
        {
            Client.Update();
        }

        public void Stop()
        {
            Client.Disconnect();
        }
    }
}
