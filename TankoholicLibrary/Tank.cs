using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TankoholicLibrary
{
    public class Tank : IEntity
    {
        public Tank(Vector2 position)
        {
            this.Position = position;
        }

        public Sprite Sprite => new ImageSprite("Images/Tank");

        public int Speed { get; private set; } = 3;

        public Vector2 Velocity { get; private set; }
        public int Health { get; private set; }

        

        public Vector2 Position { get; set; }

        public void SetVelocity(Vector2 direction)
        {
            Velocity = direction * Speed;
        }
        
        public void Update()
        {
            Position += Velocity;
        }

        public Bullet Shoot(Vector2 position, Vector2 direction)
        {
            return new Bullet(position, direction);
        }
    }
}
