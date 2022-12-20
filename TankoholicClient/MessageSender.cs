using Riptide;
using System.Collections.Generic;
using TankoholicLibrary;

namespace TankoholicClient
{
    public static class MessageSender
    {
        private static readonly List<Message> Messages = new();

        public static void SendSpawn(Player player)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.PlayerSpawn);
            message.AddString(player.Username);
            Messages.Add(message);
        }

        public static void SendPosition(Player player)
        {
            if (player.Tank is null)
            {
                return;
            }
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.PlayerPosition);
            message.AddFloats(new float[] { player.Tank.Position.X, player.Tank.Position.Y });
            Messages.Add(message);
        }

        public static void SendSpawn(Bullet bullet)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.BulletSpawn);
            message.AddFloats(new float[] { bullet.Position.X, bullet.Position.Y });
            message.AddFloats(new float[] { bullet.Direction.X, bullet.Direction.Y });
            message.AddUShort((ushort)bullet.PlayerId);
            Messages.Add(message);
        }
        
        public static void SendPosition(Bullet bullet)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.BULLET_POSITION);
            message.AddFloats(new float[] { bullet.Position.X, bullet.Position.Y });
            Messages.Add(message);
        }

        public static void SendUnpassableTileSpawn(int x, int y)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.UNPASSABLE_TILE_SPAWN);
            message.AddInts(new int[] { x, y });
            _messages.Add(message);
        }

        public static void SendUnpassableTileDespawn(int x, int y)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.UNPASSABLE_TILE_SPAWN);
            message.AddInts(new int[] { x, y });
            _messages.Add(message);
        }

        public static void SendAll()
        {
            Messages.ForEach(m => ClientNetworkManager.Instance.Client.Send(m));
            Messages.Clear();
        }
    }
}
