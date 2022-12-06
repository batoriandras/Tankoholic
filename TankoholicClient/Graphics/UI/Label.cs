using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankoholicClient
{
    public class Label : Component
    {
        public string Text { get; set; }
        public Color Color{ get; set; }

        public Label(string text, Vector2 position, Color color)
        {
            this.Text = text;
            this.Position = position;
            this.Color = color;
        }

        public void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock, ref SpriteFont spriteFont)
        {
            spriteBatch.DrawString(spriteFont, Text,
                        new Vector2(Position.X, Position.Y), Color);
        }
    }
}
