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
    }
}
