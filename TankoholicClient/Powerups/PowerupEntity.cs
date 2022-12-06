using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TankoholicClient.Powerup
{
    public class PowerupEntity : Entity
    {
        private static PowerupEffect[] allPowerupEffects = new PowerupEffect[] { 
            new SpeedUpPowerup(), new HealthPowerup()
        };

        public PowerupEffect Effect { get; private set; }

        public PowerupEntity(PowerupEffect effect)
        {
            this.Effect = effect;
        }

        public static PowerupEntity RandomPowerup()
        {
            int index = new Random().Next(allPowerupEffects.Length);
            return new PowerupEntity(allPowerupEffects[index]);
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
