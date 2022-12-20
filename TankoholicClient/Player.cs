using Microsoft.Xna.Framework;
using TankoholicClient.Entities;

namespace TankoholicClient
{
    public class Player
    {
        public Pencil.Pencil Pencil { get; set; }


        public string Username { get; set; }
        public ushort Id { get; set; }

        public Tank Tank { get; private set; }

        public Player(ushort id, string username)
        {
            Id = id;
            Username = username;
            Pencil = new Pencil.Pencil(Id, Color.Gray);
        }

        public void SetPosition(float x, float y)
        {
            Tank.Position = new Vector2(x,y);
        }

        public void SetTank(Tank tank)
        {
            Tank = tank;
        }
    }
}