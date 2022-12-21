using Riptide;
using TankoholicLibrary;

namespace TankoholicServer
{
    internal class TileManager
    {
        public static List<int[]> Tiles = new();

        [MessageHandler((ushort)MessageIds.UnpassableTileSpawn)]
        private static void UnpassableTileSpawn(ushort id, Message message)
        {
            var position = message.GetInts();
            Tiles.Add(position);
            ServerNetworkManager.Instance.Server!.SendToAll(ServerNetworkManager.CreatePositionMessage(position, MessageIds.UnpassableTileSpawn));
        }

        [MessageHandler((ushort)MessageIds.UnpassableTileDespawn)]
        private static void UnpassableTileDespawn(ushort id, Message message)
        {
            var position = message.GetInts();
            Tiles.Remove(position);

            Message message1 = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.UnpassableTileSpawn);
            message1.AddInts(position);
            ServerNetworkManager.Instance.Server!.SendToAll(message1);

        }
    }
}
