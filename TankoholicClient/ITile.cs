﻿using Microsoft.Xna.Framework;

namespace TankoholicClient
{
    public interface ITile : Sprite
    {
        public Vector2 Position { get; set; }

        public int PosX { get => (int)Position.X; }
        public int PosY { get => (int)Position.Y; }

    }
}