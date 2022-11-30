using Riptide;
using System.Collections.Generic;
using TankoholicClassLibrary;

namespace TankoholicClient
{
    public static class MessageSender
    {
        private static readonly List<Message> _messages = new();

        public static void SendSpawn(Player player)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.PLAYER_SPAWN);
            message.AddString(player.Username);
            _messages.Add(message);
        }

        public static void SendPosition(Player player)
        {
            Message message = Message.Create(MessageSendMode.Unreliable, (ushort)MessageIds.PLAYER_POSITION);
            message.AddFloats(new float[] { player.Tank.position.X, player.Tank.position.Y });
            _messages.Add(message);
        }

        public static void SendAll()
        {
            _messages.ForEach(m => ClientNetworkManager.Instance.Client.Send(m));
            _messages.Clear();
        }
    }
}
