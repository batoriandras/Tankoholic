using Microsoft.Xna.Framework;

namespace TankoholicClient.Collision
{
    public class CollisionCircle: ICollisionShape
    {
        public Vector2 CenterPosition { get; set; }
        public Vector2 Position
        {
            get => CenterPosition - new Vector2(Radius, Radius);
            set => CenterPosition = value + new Vector2(Radius, Radius);
        }
        public float Radius { get; set; }
        public float Width { get => Radius*2; set => Radius = value/2; }
        public float Height { get => Radius * 2; set => Radius = value / 2; }

        public CollisionCircle(Vector2 position, float radius)
        {
            Radius = radius;
            Position = position;
        }
    }
}
