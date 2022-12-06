using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TankoholicClient
{
    internal class Button : Component
    {
        public string Text { get; set; }

        public Color BackgroundColor { get; set; }
        public Action OnClick
        {
            get; private set;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Texture2D ImageTexture { get; private set; }


        public Button() { }

        public Button(string text, Action onClick, Vector2 position, Color backgroundColor, Texture2D image = null,
            int width = 100, int height = 50)
        {
            this.Text = text;
            this.OnClick = onClick;
            this.Position = position;
            this.ImageTexture = image;
            this.Width = width;
            this.Height = height;
            this.BackgroundColor = backgroundColor;
        }

        public void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock, ref SpriteFont spriteFont)
        {
            spriteBatch.Draw(rectangleBlock,
                        new Rectangle((int)Position.X, (int)Position.Y, Width, Height), BackgroundColor);

            spriteBatch.DrawString(spriteFont, Text,
                new Vector2(Position.X + Width / 4, Position.Y + Height / 3), Color.Black);
        }
    }
}
