using Microsoft.Xna.Framework;

namespace TankoholicClient.Graphics.Sprites
{
    public class ColorSprite : Sprite
    {
        public ColorSprite(Color color)
        {
            this.Color = color;
        }
        public Color Color { get; }
    }
}
