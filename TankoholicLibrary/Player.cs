using System.Numerics;

namespace TankoholicClassLibrary
{
    public class Player
    {
        public string Username { get; set; }
        public int Id { get; set; }

        public Vector2 Position { get; private set; }

        public Player(string username, int id)
        {
            Username = username;
            Id = id;
            Position = new Vector2(0,0);
        }

        public void SetPosition(float x, float y)
        {
            Position = new Vector2(x,y);
        }
    }
    
}