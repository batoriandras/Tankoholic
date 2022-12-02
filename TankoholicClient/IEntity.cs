using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicClient
{
    public abstract class Entity
    {
        public CollisionShape collisionShape;

        public Vector2 Position { get; set; }

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public Sprite Sprite { get; protected set; }

        public abstract void Update();

        public abstract void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock);
    }
    public enum CollisionShape
    {
        Circle,
        Rectangle
    }
}
