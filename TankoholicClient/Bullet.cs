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
        private Vector2 velocity;
        private const int MULTIPLIER = 10;
        public Vector2 Direction { get; private set; }
        public int PlayerId { get; private set; }
        public Bullet(Vector2 position, Vector2 direction, int playerId)
        {
            Position = position;
            Direction = direction;
            velocity = direction * MULTIPLIER;
            collisionShape = CollisionShape.Circle;
            Width = 20;
            Height = 20;
            PlayerId = playerId;
        }
        public override void Update()
        {
            Position += velocity;
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(rectangleBlock,
                new Rectangle((int)Position.X, (int)Position.Y,
                    Width, Height),
                Color.Gray);
        }
    }
}
