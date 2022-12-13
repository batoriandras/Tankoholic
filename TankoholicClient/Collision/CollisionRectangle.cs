using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicClient
{
    public class CollisionRectangle : ICollisionShape
    {
        public Vector2 Position { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public CollisionRectangle(Vector2 position, float width, float height)
        {
            Position = position;
            Width = width;
            Height = height;
        }


        public bool CheckCircleCollision(CollisionCircle otherCircle)
        {
            throw new NotImplementedException();
        }

        public bool CheckRectangleCollision(CollisionRectangle otherRectangle)
        {
            throw new NotImplementedException();
        }
    }
}
