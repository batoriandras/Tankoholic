using Riptide;
using Riptide.Utils;
using TankoholicLibrary;

namespace TankoholicServer
{
    internal class ServerNetworkManager
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


        public Riptide.Server Server { get; private set; }

        private ushort port = 7070;
        private ushort maxClient = 2;

        public void Initialize()
        {
            RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.WriteLine, false);

            Server = new Server();
            Server.Start(port, maxClient);

            Server.ClientConnected += (s, e) => PlayerManager.SendSpawnMessage(e.Client.Id, "Janika");
            Server.ClientDisconnected += (s, e) => PlayerManager.PlayerIds.Remove(e.Client.Id);
        }

        public void Update()
        {
            Server.Update();
        }

        public void Stop()
        {
            Server.Stop();
        }

        public static Message CreatePositionMessage(ushort id, float[] position, MessageIds messageId)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)messageId);
            message.AddUShort(id);
            message.AddFloats(position);
            return message;
        }

        public static Message CreatePositionMessage(int[] position, MessageIds messageId)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)messageId);
            message.AddInts(position);
            return message;
        }
    }
}
