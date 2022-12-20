using Microsoft.Xna.Framework;

namespace TankoholicClient.Collision
{
    public interface ICollisionShape
    {
        public Vector2 Position { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }
    }
}
