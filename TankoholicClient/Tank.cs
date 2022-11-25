﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TankoholicClient;

namespace TankoholicClient
{ 
    public class Tank : Entity
    {
        public Tank(Vector2 position)
        {
            this.Position = position;
        }

        public int Speed { get; private set; } = 3;

        public Vector2 Velocity { get; private set; }
        public int Health { get; private set; }


        public void SetVelocity(Vector2 direction)
        {
            Velocity = direction * Speed;
        }

        void Shoot()
        {
            
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