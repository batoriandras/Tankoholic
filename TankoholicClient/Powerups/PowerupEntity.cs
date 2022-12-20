using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TankoholicClient.Collision;
using TankoholicClient.Graphics.Sprites;
using TankoholicClient.Graphics.Tiles;
using TankoholicClient.Powerups.Effects;

namespace TankoholicClient.Powerups
{
    public class PowerupEntity : Entity
    {
        private static int idCounter = 0;
        public int Id { get; private set; }

        private static PowerupEffect[] allPowerupEffects = new PowerupEffect[] { 
            new SpeedUpPowerup(), new HealthPowerup()
        };

        public PowerupEffect Effect { get; private set; }

        public PowerupEntity(PowerupEffect effect, Vector2 position)
        {
            idCounter++;
            Id = idCounter;

            this.Effect = effect;
            this.Position = position;
            Sprite = new ColorSprite(Color.Violet);
            
            Width = 22;
            Height = 22;
            CollisionShape = new CollisionRectangle(position, Width, Height);
        }

        public static PowerupEntity RandomPowerup()
        {
            int index = new Random().Next(allPowerupEffects.Length);
            List<PassableTile> emptyTiles = MapManager.Instance.EmptyTiles();
            PassableTile randomTile = emptyTiles[new Random().Next(emptyTiles.Count)];
            Vector2 position = randomTile.Position;
            return new PowerupEntity(allPowerupEffects[index], position);
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(rectangleBlock,
                new Rectangle((int)Position.X, (int)Position.Y,
                Width, Height),
                ((ColorSprite)Sprite).Color);
        }

        public override void Update()
        {
            
        }
    }
}
