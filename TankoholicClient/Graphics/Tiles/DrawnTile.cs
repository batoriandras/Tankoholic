using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TankoholicClient
{
    public class DrawnTile : UnpassableTile
    {
        public int PlayerId { get; set; }
        public Color Color { get; set; }

        public DrawnTile(int playerId, Color color)
        {
            PlayerId = playerId;
            Color = color;
        }

        public static DrawnTile FromPencil(Pencil pencil)
        {
            return new DrawnTile(pencil.playerId, pencil.Color);
        }

        public Vector2 Position { get; set; }

        public Sprite Sprite => throw new NotImplementedException();

        public void Update()
        {
            // tile doesnt update
        }

        public void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(rectangleBlock, new Rectangle(
                50, 50,
                // (int)Position.X, (int)Position.Y,
                (GameConstants.CELL_SIZE * 10), (GameConstants.CELL_SIZE) * 10),
                //  (Sprite as ColorSprite).Color
                Color.Red
                );
        }

    }
}
