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

        public static void SendName()
        {
            // MessageSendMode.Reliable az TCP protokolt használ, ami azt jelenti hogy biztosra fog menni hogy az üzenet átment-e. Ez lassabb
            // MessageSendMode.Unreliable az UDP protokolt használ, amit nem érdekli hogy az üzenet megérkezett-e. Ez gyorsabb

            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.PLAYER_NAME);
            message.AddString("Pista");
            ClientNetworkManager.Instance.Client.Send(message);
        }

        public static void AddPosition(Player player)
        {
            // MessageSendMode.Reliable az TCP protokolt használ, ami azt jelenti hogy biztosra fog menni hogy az üzenet átment-e. Ez lassabb
            // MessageSendMode.Unreliable az UDP protokolt használ, amit nem érdekli hogy az üzenet megérkezett-e. Ez gyorsabb

            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageIds.PLAYER_POSITION);
            message.AddFloats(new float[] { player.Position.X, player.Position.Y }, false);
            messages.Add(message);
            
        }


        public static void SendAll()
        {
            messages.ForEach(m=> ClientNetworkManager.Instance.Client.Send(m));
        }
    }
}
