using Microsoft.Xna.Framework;

namespace TankoholicClient
{
    public class Player
    {
        public string Username { get; set; }
        public int Id { get; set; }

        
        public Tank Tank { get; set; }

        public Pencil pencil { get; set; }

        public Player(string username, int id)
        {
            Username = username;
            Id = id;
            Tank = new Tank(position: new Vector2(100, 100));
            pencil = new Pencil(Id, Microsoft.Xna.Framework.Color.Gray);
        }
    }
}