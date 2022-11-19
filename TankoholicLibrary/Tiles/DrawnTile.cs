using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankoholicClient;

namespace TankoholicLibrary
{
    public class DrawnTile : UnpassableTile
    {
        int playerId;
        Color color;
        public DrawnTile(Pencil pencil)
        {
            playerId = pencil.playerId;
            color = pencil.Color;
        }

        Sprite IEntity.Sprite => throw new NotImplementedException();


        Vector2 IEntity.Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        void IEntity.Update()
        {
            throw new NotImplementedException();
        }
    }
}
