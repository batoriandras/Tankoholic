using Riptide;
using TankoholicClassLibrary;

namespace TankoholicServer
{
    public static class PlayerManager
    {
        static PlayerManager() { }


        public static List<Player> players = new List<Player>();

        public static void Spawn(ushort id, string username)
        {
            Player player = new Player(username, id);
            players.Add(player);

            Console.WriteLine("Player added: " + username);
        }

        [MessageHandler((ushort)ClientToServerId.name)]
        private static void Name(ushort fromClientId, Message message)
        {
            Spawn(fromClientId, message.GetString());
        }
    }
}
