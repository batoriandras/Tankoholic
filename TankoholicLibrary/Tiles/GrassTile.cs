﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicLibrary
{
    public class GrassTile : PassableTile
    {
        public Vector2 Position { get; set; }

        public string SpriteName => throw new NotImplementedException();

        public Sprite Sprite { get => new ColorSprite(Color.Green); }

        public GrassTile(Vector2 position)
        {
            Position = position;
        }

        public void Update()
        {
            
        }
    }
}
