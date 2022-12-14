using Riptide;
using System.Collections.Generic;
using TankoholicClient.Entities;
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

        public static void SendPosition(Tank tank)
        {
            if (tank is null)
            {
                return;
            }
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.PlayerPosition);
            message.AddFloats(new float[] { tank.Position.X, tank.Position.Y });
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
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.BulletPosition);
            message.AddFloats(new float[] { bullet.Position.X, bullet.Position.Y });
            Messages.Add(message);
        }

        public static void SendUnpassableTileSpawn(int x, int y)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.UnpassableTileSpawn);
            message.AddInts(new int[] { x, y });
            Messages.Add(message);
        }

        public static void SendUnpassableTileDespawn(int x, int y)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.UnpassableTileDespawn);
            message.AddInts(new int[] { x, y });
            Messages.Add(message);
        }

        public static void SendAll()
        {
            Messages.ForEach(m => ClientNetworkManager.Instance.Client.Send(m));
            Messages.Clear();
        }
    }
}
