using TankoholicLibrary;
using Riptide;

namespace TankoholicServer
{
    internal static class PlayerManager
    {
        public static readonly List<ushort> PlayerIds = new();

        public static void SendSpawnMessage(ushort id, string username)
        {
            PlayerIds.Add(id);
            ushort[] ids = new ushort[PlayerIds.Count];
            for(int i = 0; i < PlayerIds.Count; i++)
                ids[i] = PlayerIds[i];

            var spawnMessage = CreateSpawnMessage(ids, username);

            string tilePositions = "";
            for(int i = 0; i < TileManager.Tiles.Count; i++)
            {
                var pos = TileManager.Tiles[i];
                tilePositions += $"{pos[0]},{pos[1]}";

                if (i != TileManager.Tiles.Count - 1)
                    tilePositions += ";";
            }
            spawnMessage.AddString(tilePositions);
            ServerNetworkManager.Instance.Server!.SendToAll(spawnMessage);

            ServerDebug.Info($"Player joined! Id: {id} Username: {username}");
        }

        [MessageHandler((ushort)MessageIds.PlayerPosition)]
        private static void Position(ushort id, Message message)
        {
            var position = message.GetFloats();
            ServerNetworkManager.Instance.Server!.SendToAll(ServerNetworkManager.CreatePositionMessage(id, position, MessageIds.PlayerPosition));

            if (!ServerDebug.DebugPosition) return;
            ServerDebug.Warn($"Player({id}) new position: X:{position[0]} Y:{position[1]}");
        }

        private static Message CreateSpawnMessage(ushort[] id, string username)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.PlayerSpawn);
            message.AddUShorts(id);
            message.AddString(username);
            return message;
        }
    }
}
