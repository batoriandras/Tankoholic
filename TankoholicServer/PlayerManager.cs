using Riptide;
using TankoholicClassLibrary;

namespace TankoholicServer
{
    public static class PlayerManager
    {
        public static readonly List<ushort> PlayerIds = new();

        public static void SendSpawnMessage(ushort id, string username)
        {
            PlayerIds.Add(id);
            ushort[] ids = new ushort[PlayerIds.Count];
            for(int i = 0; i < PlayerIds.Count; i++)
                ids[i] = PlayerIds[i];
            /* Egyenlőre minden Player ugyanazt a nevet kapja */
            Program.Server!.SendToAll(CreateSpawnMessage(ids, username));

            ServerDebug.Info($"Player joined! Id: {id} Username: {username}");
        }

        [MessageHandler((ushort)MessageIds.PLAYER_POSITION)]
        private static void Position(ushort id, Message message)
        {
            var position = message.GetFloats();
            Program.Server!.SendToAll(CreatePositionMessage(id, position));

            if (!Program.DebugPosition) return;
            ServerDebug.Warn($"Player({id}) new position: X:{position[0]} Y:{position[1]}");
        }

        private static Message CreateSpawnMessage(ushort[] id, string username)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.PLAYER_SPAWN);
            message.AddUShorts(id);
            message.AddString(username);
            return message;
        }

        private static Message CreatePositionMessage(ushort id, float[] position)
        {
            Message message = Message.Create(MessageSendMode.Reliable, MessageIds.PLAYER_POSITION);
            message.AddUShort(id);
            message.AddFloats((float[])position);
            return message;
        }
    }
}
