using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TankoholicClient.Collision;

namespace TankoholicClient.Graphics.Tiles
{
    public class DrawnTile : UnpassableTile
    {
        public Color Color { get; set; }

        public DrawnTile(Color color, Vector2 position)
        {
            Color = color;

            Width = GameConstants.CELL_SIZE;
            Height = GameConstants.CELL_SIZE;
            CollisionShape = new CollisionRectangle(position, Width, Height);
            Position = position;
        }

        public static DrawnTile FromPencil(Pencil.Pencil pencil, Vector2 position)
        {
            return new DrawnTile(pencil.Color, position);
        }

        public override void Update()
        {
            
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(rectangleBlock, new Rectangle(
                (int)Position.X, (int)Position.Y,
                Width, Height),
                Color
                );
        }
    }
}
