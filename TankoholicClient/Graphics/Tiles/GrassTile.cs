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
        public Vector2 Position { get; set; }

        public string SpriteName => throw new NotImplementedException();

        public Sprite Sprite { get => new ColorSprite(Color.Green); }

        public GrassTile(Vector2 position)
        {
            Position = position;
        }

        public void Update()
        {
            
        }

        public void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(rectangleBlock, new Rectangle(
                (int)Position.X, (int)Position.Y,
                (GameConstants.CELL_SIZE * 10), (GameConstants.CELL_SIZE) * 10),
                (Sprite as ColorSprite).Color);
        }
    }
}
