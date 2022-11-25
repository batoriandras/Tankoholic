using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicClient
{
    public class ImageSprite : Sprite
    {
        public ImageSprite(string name)
        {
            this.Name = name;
        }
        public string Name { get; }
    }
}
