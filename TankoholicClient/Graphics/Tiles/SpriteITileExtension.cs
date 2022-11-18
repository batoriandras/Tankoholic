using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TankoholicLibrary;

namespace TankoholicClient
{
    public static class SpriteITileExtension
    {
        public static void Draw(this ITile tile, ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(rectangleBlock, new Rectangle(
                (int)tile.Position.X, (int)tile.Position.Y,
                (GameConstants.CELL_SIZE * 10), (GameConstants.CELL_SIZE) * 10),
                (tile.Sprite as ColorSprite).Color);
        }
    }
}
