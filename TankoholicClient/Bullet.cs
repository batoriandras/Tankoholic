using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace TankoholicClient
{
    public class Bullet:Entity
    {
        public Vector2 Position { get; private set; }
        private Vector2 velocity;
        private const int MULTIPLIER = 10;
        public Bullet(Vector2 position, Vector2 direction)
        {
            Position = position;
            velocity = direction * MULTIPLIER;
        }
        public override void Update()
        {
            Position += velocity;
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(rectangleBlock,
                new Rectangle((int)Position.X, (int)Position.Y,
                    40, 40),
                Color.Gray);
        }
    }
}
