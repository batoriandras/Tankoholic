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

        [MessageHandler((ushort)MessageIds.PLAYER_NAME)]
        private static void Name(ushort fromClientId, Message message)
        {
            Spawn(fromClientId, message.GetString());
        }

        [MessageHandler((ushort)MessageIds.PLAYER_POSITION)]
        private static void Position(ushort fromClientId, Message message)
        {
          //  Console.WriteLine(fromClientId);
            if (fromClientId == 2)
            {
                players.ForEach(x => x.SetPosition(message.GetFloats()[0], message.GetFloats()[1]));
                Console.WriteLine("Player pos: " + players[0].Position.X + ", " + players[0].Position.Y);
            }
        }
    }
}
