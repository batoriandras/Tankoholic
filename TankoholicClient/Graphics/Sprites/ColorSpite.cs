using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicClient
{
    public class ColorSprite : Sprite
    {
        public ColorSprite(Color color)
        {
            this.Color = color;
        }
        public Color Color { get; }
    }
}
