using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TankoholicClient;

namespace TankoholicClient
{ 
    public class Tank : Entity
    {
        public ushort PlayerId { get; private set; }
        public Tank(Vector2 position, ushort playerId)
        {
            CanShoot = true;
            Position = position;
            collisionShape = CollisionShape.Circle;
            Width = 40;
            Health = 4;
        }

        public int Speed { get; private set; } = 2;

        public Vector2 Velocity { get; private set; }
        public int Health { get; private set; }
        public bool CanShoot { get; private set; }

        public void LoseHealth()
        {
            Health--;
        }
        public void SetVelocity(Vector2 direction)
        {
            Velocity = direction * Speed;
        }

        public Bullet Shoot(Vector2 direction)
        {
            return new Bullet(Position, direction, EntityManager.Tank.PlayerId);
        }

        public void ToggleCanShoot()
        {
            if (CanShoot == true)
            {
                CanShoot = false;
            }
            else
            {
                CanShoot = true;
            }
        }

        public override void Update()
        {
            Position += Velocity;
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(rectangleBlock,
                new Rectangle((int)Position.X, (int)Position.Y,
                40, 40),
                Color.Black);
        }
    }


}
