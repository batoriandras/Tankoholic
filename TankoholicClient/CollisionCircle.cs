using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicClient
{
    public class CollisionCircle: ICollisionShape
    {
        public Vector2 CenterPosition { get; set; }
        public Vector2 Position
        {
            get
            {
                return CenterPosition - new Vector2(Radius, Radius);
            }
            set
            {
                CenterPosition = value + new Vector2(Radius, Radius);
            }
        }
        public float Radius { get; set; }
        public CollisionCircle(float radius, Vector2 position)
        {
            Radius = radius;
            Position = position;
        }
        public bool CheckCircleCollision(CollisionCircle otherCircle)
        {
            Vector2 centerDistance = CenterPosition - otherCircle.CenterPosition;
            float centerDistanceSq = (float)(Math.Pow(centerDistance.X, 2) + Math.Pow(centerDistance.Y, 2));
            float radiusSq = (float)Math.Pow(Radius + otherCircle.Radius, 2);
            return centerDistanceSq <= radiusSq;
        }

        public bool CheckRectangleCollision(CollisionRectangle otherRectangle)
        {
            throw new NotImplementedException();
        }
    }
}
