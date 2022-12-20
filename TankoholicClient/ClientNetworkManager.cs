using System;
using Riptide;
using Riptide.Utils;

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
            Client.ClientDisconnected += (s, e) => EntityManager.RemoveTank(e.Id);
            Client.Connected += DidConnect;
            Client.ConnectionFailed += FailedToConnect;
        
            
        }

        public void Connect()
        {
            Client.Connect($"{ip}:{port}");
        }

        private void DidConnect(object sender, EventArgs e)
        {

        }
        private void FailedToConnect(object sender, EventArgs e)
        {

        }

        private void DidDisconnect(object sender, EventArgs e)
        {

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
