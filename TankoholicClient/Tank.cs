using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TankoholicClient;
using TankoholicClient.Powerup;

namespace TankoholicClient
{
    public class Tank : Entity
    {
        public Tank(Vector2 position)
        {
            this.Position = position;
            sprite = new ColorSprite(Color.Black);
            ApplyPowerup(new SpeedUpPowerup());
        }

        public int InitialSpeed { get; private set; } = 2;
        public int CurrentSpeed { get; private set; } = 2;

        public Vector2 Velocity { get; private set; }
        public int MaxHealth { get; private set; } = 4;

        private int currentHealth = 4;
        public int CurrentHealth
        {
            get => currentHealth;
            private set
            {
                if (value + currentHealth <= MaxHealth)
                {
                    currentHealth += value;
                }
            }
        }

        public PowerupEffect AppliedPowerup { get; private set; }

        public void SetVelocity(Vector2 direction)
        {
            Velocity = direction * CurrentSpeed;
        }

        void Shoot()
        {

        }

        public override void Update()
        {
            Position += Velocity;
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(rectangleBlock,
                new Rectangle((int)Position.X, (int)Position.Y,
                40, 40),
                ((ColorSprite)sprite).Color);
        }

        void DisposePowerup()
        {
            AppliedPowerup = null;
        }

        void ApplyPowerup(PowerupEffect powerup)
        {
            AppliedPowerup = powerup;

            if (AppliedPowerup is PowerupEffectTemporary tempPowerup)
            {
                if (AppliedPowerup is SpeedUpPowerup speedUpPowerup)
                {
                    CurrentSpeed = speedUpPowerup.speed;
                    sprite = new ColorSprite(Color.Red);
                    speedUpPowerup.OnEnd = delegate ()
                    {
                        sprite = new ColorSprite(Color.Black);
                        CurrentSpeed = InitialSpeed;
                        DisposePowerup();
                    };
                }
                tempPowerup.Start();
            }

            else if (AppliedPowerup is PowerupEffectPermanent permanentPowerup)
            {
                if (AppliedPowerup is HealthPowerup healthPowerup)
                {
                    CurrentHealth += healthPowerup.healthRegain;
                    DisposePowerup();
                }
            }
        }
    }
}
