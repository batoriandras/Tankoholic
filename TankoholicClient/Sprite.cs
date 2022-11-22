using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankoholicClient
{
    public interface Sprite
    {
        public void Update();
        public void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock);
        public Color Color{ get; }
    }
}
