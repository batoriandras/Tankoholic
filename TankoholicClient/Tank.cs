using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankoholicClient
{
    public class Tank : Entity
    {
        public Tank(Vector2 position)
        {
            this.Position = position;
            Sprite = new ColorSprite(Color.Black);
            ApplyPowerup(new SpeedUpPowerup());
            collisionShape = CollisionShape.Circle;
            Width = 40;
            Height = 40;
        }

        public float InitialSpeed { get; private set; } = 2.8f;
        public float CurrentSpeed { get; private set; } = 2.8f;

        public float InitialDamage { get; private set; } = 5;
        public float CurrentDamage { get; private set; } = 5;

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
            Bullet bullet = Bullet(new Vector2(0, 0), new Vector2(0, 0), CurrentDamage);
        }

        public override void Update()
        {
            Position += Velocity;
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(rectangleBlock,
                new Rectangle((int)Position.X, (int)Position.Y,
                Width, Height),
                ((ColorSprite)Sprite).Color);
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
                    Sprite = new ColorSprite(Color.Red);
                    speedUpPowerup.OnEnd = delegate ()
                    {
                        Sprite = new ColorSprite(Color.Black);
                        CurrentSpeed = InitialSpeed;
                        DisposePowerup();
                    };
                }
                if (AppliedPowerup is BulletPowerup bulletPowerup)
                {
                    CurrenDamage = bulletPowerup.damage;
                    Sprite = new ColorSprite(Color.Blue);
                    bulletPowerup.OnEnd = delegate ()
                    {
                        Sprite = new ColorSprite(Color.Black);
                        CurrentDamage = InitialDamage;
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
