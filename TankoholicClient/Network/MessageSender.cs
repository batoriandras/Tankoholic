using Microsoft.Xna.Framework;
using Riptide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankoholicClassLibrary;

namespace TankoholicClient
{
    public static class MessageSender
    {

        static List<Message> messages = new List<Message>();

        public static void SendName(Player player)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.PLAYER_NAME);
            message.AddString(player.Username);
            ClientNetworkManager.Instance.Client.Send(message);
        }

        public static void AddPosition(Player player)
        {
            Message message = Message.Create(MessageSendMode.Unreliable, (ushort)MessageIds.PLAYER_POSITION);
            message.AddFloats(new float[] { player.Tank.Position.X, player.Tank.Position.Y }, false);
            messages.Add(message);
        }

        public static void SendAll()
        {
            messages.ForEach(m=> ClientNetworkManager.Instance.Client.Send(m));
        }
    }
}
