using System;
using TankoholicClient.Entities;

namespace TankoholicClient.Graphics.Tiles
{
    public abstract class Tile : Entity
    {
        public Tuple<int, int> GetCellPosition()
        {
            int x = (int)Math.Floor(Position.X / GameConstants.CELL_SIZE);
            int y = (int)Math.Floor(this.Position.Y / GameConstants.CELL_SIZE);
            Tuple<int, int> cellPosition = new Tuple<int, int>(x, y);
            return cellPosition;
        }
    }
}
