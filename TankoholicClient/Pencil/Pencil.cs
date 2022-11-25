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


        public Pencil(int playerId, Color color)
        {
            this.playerId = playerId;
            Color = color;
        }
    }
}
