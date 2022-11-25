﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TankoholicClient
{
    public class DrawnTile : UnpassableTile
    {
        public int PlayerId { get; set; }
        public Color Color { get; set; }

        public DrawnTile(int playerId, Color color, Vector2 position)
        {
            PlayerId = playerId;
            Color = color;

            this.position = position;

            
        }

        public static DrawnTile FromPencil(Pencil pencil, Vector2 position)
        {
            return new DrawnTile(pencil.playerId, pencil.Color, position);
        }

        public override void Update()
        {
            
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(rectangleBlock, new Rectangle(
                (int)Position.X, (int)Position.Y,
                GameConstants.CELL_SIZE, GameConstants.CELL_SIZE),
                Color
                );
        }
    }
}