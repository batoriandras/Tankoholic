using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicClient
{
    public abstract class ITile : Entity
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
