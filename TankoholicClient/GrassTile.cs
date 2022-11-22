using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicClient
{





    public class SpriteGrassTile : GrassTile, SpriteITile
    {
        public SpriteGrassTile(Vector2 position) : base(position)
        {
        }


        public void Update()
        {
            // Grass doesn't change, so there is no need to update it.
        }

        public void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(rectangleBlock,
                        new Rectangle((int)Position.X * GameConstants.CELL_SIZE, (int)Position.Y * GameConstants.CELL_SIZE,
                                      GameConstants.CELL_SIZE, GameConstants.CELL_SIZE), Color);
        }
    }


    public class GrassTile : PassableTile
    {
        public Color Color { get => Color.Green;}
        public Vector2 Position { get; set; }

        public GrassTile(Vector2 position)
        {
            Position = position;
        }

        

    }
}
