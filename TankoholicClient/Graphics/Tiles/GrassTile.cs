using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicClient
{
    public class GrassTile : PassableTile
    {
        public GrassTile(Vector2 position)
        {
            Sprite = new ColorSprite(Color.Green);
            Position = position;
        }

        public override void Update()
        {
          
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(rectangleBlock, new Rectangle(
                (int)Position.X, (int)Position.Y,
                (GameConstants.CELL_SIZE * 10), (GameConstants.CELL_SIZE) * 10),
                (Sprite as ColorSprite).Color);
        }
    }
}
