using Riptide;
using TankoholicClassLibrary;

namespace TankoholicServer
{
    internal class TileManager
    {
        public static List<int[]> Tiles = new();

        [MessageHandler((ushort)MessageIds.UNPASSABLE_TILE_SPAWN)]
        private static void UnpassableTileSpawn(ushort id, Message message)
        {
            var position = message.GetInts();
            Tiles.Add(position);
            Program.Server!.SendToAll(ServerNetworkManager.CreatePositionMessage(position, MessageIds.UNPASSABLE_TILE_SPAWN));
        }

        [MessageHandler((ushort)MessageIds.UNPASSABLE_TILE_DESPAWN)]
        private static void UnpassableTileDespawn(ushort id, Message message)
        {
            var position = message.GetInts();
            Tiles.Remove(position);

            Message message1 = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.UNPASSABLE_TILE_SPAWN);
            message1.AddInts(position);
            Program.Server!.SendToAll(message1);

        }
    }
}
