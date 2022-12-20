using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankoholicClient.Graphics.UI
{
    internal class Image : Component
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Texture2D ImageTexture { get; private set; }


        public Image(Vector2 position, Texture2D image = null,
            int width = 48, int height = 48)
        {
            this.Position = position;
            this.ImageTexture = image;
            this.Width = width;
            this.Height = height;
        }

        public void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(ImageTexture, Position, Color.White);
        }
    }
}
