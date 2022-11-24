using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicLibrary
{
    public class Bullet
    {
        public Vector2 Position { get; private set; }
        private Vector2 velocity;
        private const int MULTIPLIER = 10;
        public Bullet(Vector2 position, Vector2 direction)
        {
            Position = position;
            velocity = direction * MULTIPLIER;
        }
        public void Update()
        {
            Position += velocity;
        }
    }
}
