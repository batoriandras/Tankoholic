using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicLibrary
{
    public interface IEntity
    {
        public Sprite Sprite { get;}

        public Vector2 Position { get; protected set; }

        public void Update();
    }
}
