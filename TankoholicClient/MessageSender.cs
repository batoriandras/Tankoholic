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
            if (player.Tank is null)
            {
                return;
            }
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.PLAYER_POSITION);
            message.AddFloats(new float[] { player.Tank.Position.X, player.Tank.Position.Y });
            _messages.Add(message);
        }

        public static void SendSpawn(Bullet bullet)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.BULLET_SPAWN);
            message.AddFloats(new float[] { bullet.Position.X, bullet.Position.Y });
            message.AddFloats(new float[] { bullet.Direction.X, bullet.Direction.Y });
            message.AddUShort((ushort)bullet.PlayerId);
            _messages.Add(message);
        }
        public static void SendPosition(Bullet bullet)
        {
            Message message = Message.Create(MessageSendMode.Unreliable, (ushort)MessageIds.BULLET_POSITION);
            message.AddFloats(new float[] { bullet.Position.X, bullet.Position.Y });
            _messages.Add(message);
        }

        public static void SendAll()
        {
            _messages.ForEach(m => ClientNetworkManager.Instance.Client.Send(m));
            _messages.Clear();
        }
    }
}
