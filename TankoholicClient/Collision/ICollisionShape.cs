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
        public Vector2 Position { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }
    }
}
