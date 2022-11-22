using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TankoholicLibrary
{
    public class Tank
    {
        public Vector2 position { get; set; }
        Vector2 velocity;

        int health;

        public void Move(Vector2 direction)
        {
            velocity = direction;
            UpdateLogic();
        }

        void Shoot()
        {

        }

        public void UpdateLogic()
        {
            position += velocity;
            velocity = new Vector2(0,0);
        }
    }
}
