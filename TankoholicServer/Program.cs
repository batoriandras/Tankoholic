using Riptide;
using Riptide.Utils;

namespace TankoholicServer
{
    internal class Program
    {
        internal static Server? Server { get; private set; }

        private static void Main()
        {
            RiptideLogger.Initialize(Console.WriteLine, false);

            Server = new Server();
            Server.Start(7070, 2);

            Server.ClientConnected += (s, e) => PlayerManager.SendSpawnMessage(e.Client.Id, "Janika");
            Server.ClientDisconnected += (s, e) => PlayerManager.PlayerIds.Remove(e.Client.Id);

            while (true)
            {
                Server.Update();
                Thread.Sleep(10);
            }
        }
    }
}