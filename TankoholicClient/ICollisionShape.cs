using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicClient
{
    public interface ICollisionShape
    {
        Vector2 Position { get; set; }
        bool CheckRectangleCollision(CollisionRectangle otherRectangle);
        bool CheckCircleCollision(CollisionCircle otherCircle);
    }
}
