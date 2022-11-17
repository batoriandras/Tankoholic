using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicLibrary
{
    public class Tank
    {
        public Bullet Shoot(Vector2 position, Vector2 direction) // Az irány valószínüleg a tank-tól fog jönni
        {
            return new Bullet(position, direction);
        }
    }
}
