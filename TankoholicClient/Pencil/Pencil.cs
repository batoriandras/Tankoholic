using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicClient
{
    public class Pencil
    {
        public int playerId;
        private const int pencilSize = 5;
        public Color Color { get; set; }

        public ITile tileToDraw;

        public Pencil(int playerId, Color color, ITile tileToDraw)
        {
            this.playerId = playerId;
            Color = color;
            this.tileToDraw = tileToDraw;
        }
    }
}
