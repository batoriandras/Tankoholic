using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankoholicLibrary;

namespace TankoholicClient
{
    public static class GrassTileDrawExtension
    {
        public static void Draw(this GrassTile grassTile, ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(rectangleBlock, new Rectangle(
                (int)grassTile.Position.X, (int)grassTile.Position.Y,                
                (GameConstants.CELL_SIZE * 10), (GameConstants.CELL_SIZE) * 10),
                Color.Green);
        }
    }
}
