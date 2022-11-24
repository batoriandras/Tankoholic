using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TankoholicClient;

namespace TankoholicClient
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

        void Shoot()
        {
            
        }

        public void Draw( ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(rectangleBlock,
                new Rectangle((int)Position.X, (int)Position.Y,
                (GameConstants.CELL_SIZE * 10), (GameConstants.CELL_SIZE) * 10),
                Color.Black);
        }
    }
}
