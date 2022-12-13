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


        public string Username { get; set; }
        public ushort Id { get; set; }

        public Tank Tank { get; private set; }

        public Player(ushort id, string username)
        {
            Id = id;
            Username = username;
            pencil = new Pencil(Id, Microsoft.Xna.Framework.Color.Gray);
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