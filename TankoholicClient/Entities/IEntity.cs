using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TankoholicClient.Collision;
using TankoholicClient.Graphics.Sprites;

namespace TankoholicClient.Entities
{
    public abstract class Entity
    {
        private Vector2 position;
        public Vector2 Position { get => position;
            set { 
                position = value;
                if (CollisionShape != null)
                {
                    CollisionShape.Position = position;
                }
                
            }
        }

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public Sprite Sprite { get; protected set; }
        public ICollisionShape CollisionShape { get; set; }

        public abstract void Update();

        public abstract void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock);
    }
    public enum CollisionShape
    {
        Circle,
        Rectangle
    }
}
