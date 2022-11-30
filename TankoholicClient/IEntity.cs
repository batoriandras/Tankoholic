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
        public Vector2 position;
        public int width, height;
        public Sprite sprite;
        public ICollisionShape collisionShape;

        public Vector2 Position { get => position; set => position = value; }

        public int Width { get => width; }
        public int Height { get => height; }

        public Sprite Sprite { get => sprite; }

        public abstract void Update();

        public abstract void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock);
    }
}
