using Riptide;
using TankoholicClient;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace TankoholicClassLibrary
{
    public class Player
    {
        public Pencil pencil { get; set; }

        public static Dictionary<ushort, Player> OtherPlayers = new();

        public string Username { get; set; }
        public ushort Id { get; set; }

        public Tank Tank { get; set; }

        public Player(ushort id, string username)
        {
            Id = id;
            Username = username;
            Tank = new Tank(position: new Vector2(100, 100));
            pencil = new Pencil(Id, Microsoft.Xna.Framework.Color.Gray);
        }

        public void SetPosition(float x, float y)
        {
            Tank.Position = new Vector2(x,y);
        }

        

        [MessageHandler((ushort)MessageIds.PLAYER_POSITION)]
        private static void HandlePosition(Message message)
        {
            var id = message.GetUShort();
            if (id == ClientNetworkManager.Instance.Client.Id) return;

            var pos = message.GetFloats();

            if (OtherPlayers.TryGetValue(id, out Player otherPlayer))
            {
                otherPlayer.Tank.Position = new Vector2(pos[0], pos[1]);
            }
        }

        [MessageHandler((ushort)MessageIds.PLAYER_SPAWN)]
        private static void HandleSpawn(Message message)
        {
            var ids = message.GetUShorts();
            var username = message.GetString();

            /* Ha valakinek van erre valami jobb megoldása akkor ne legyen rest átírni */
            if (ids.Length == 1)
            {
                OtherPlayers.Add(ids[0], new Player(ids[0], username));
            }
            else
            {
                if(OtherPlayers.Count == 1)
                {
                    var id = ids[0] == ClientNetworkManager.Instance.Client.Id ? ids[1] : ids[0];
                    OtherPlayers.Add(id, new Player(id, username));
                }
                else
                {
                    foreach(var id in ids)
                        OtherPlayers.Add(id, new Player(id, username));
                }
            }
        }
    }
}