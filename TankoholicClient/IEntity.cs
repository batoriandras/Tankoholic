using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicClient
{
    public interface IEntity
    {
        public void Update();

        public void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock);



       // A TILE NEM TUDTHAT ÖNMAGA POZICIÓJÁRÓL!
        public Vector2 Position { get; set; }

        public Sprite Sprite { get; }
    }
}
