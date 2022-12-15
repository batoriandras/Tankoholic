using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicClient
{
    public class Bullet
    {
        public Vector2 Position { get; private set; }
        private Vector2 velocity;
        private const int MULTIPLIER = 10;
        public float Damage { get; set; }
        public Bullet(Vector2 position, Vector2 direction, float damage = 5)
        {
            Position = position;
            velocity = direction * MULTIPLIER;
            this.Damage = damage;
        }


        public void Update()
        {
            Position += velocity;
        }
    }
}
