using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Timer = System.Timers.Timer;

namespace TankoholicClient
{
    public class Tank : Entity
    {        
        private readonly int shootingCooldown;
        private Timer shootingTimer;
        private Timer damageTakenTimer;
        public ushort PlayerId { get; private set; }
        public Tank(Vector2 position, ushort playerId)
        {
            Sprite = new ColorSprite(Color.Black);
            Width = 40;
            Height = 40;
            CollisionShape = new CollisionCircle(position, (float)Width / 2);
            CanShoot = true;
            Position = position;
            PlayerId = playerId;
            shootingCooldown = 1;
            InitializeTimers();
        }

        public float InitialSpeed { get; private set; } = 2.8f;
        public float CurrentSpeed { get; private set; } = 2.8f;

        public int InitialDamage { get; private set; } = 1;
        public int CurrentDamage { get; private set; } = 1;

        public Vector2 Velocity { get; private set; }

        public int MaxHealth { get; private set; } = 4;

        private int currentHealth = 4;
        public int CurrentHealth
        {
            get => currentHealth;
            private set
            {
                if (value <= MaxHealth)
                {
                    currentHealth = value;
                }
                else
                {
                    currentHealth = MaxHealth;
                }
            }
        }

        public PowerupEffect AppliedPowerup { get; private set; }
        
        public bool CanShoot { get; private set; }

        public void SpawnEnemy()
        {
            Sprite = new ColorSprite(Color.Black);
            CanShoot = true;
            Position = new Vector2(GameConstants.WINDOW_WIDTH/4*3, GameConstants.WINDOW_HEIGHT/2);
            CurrentHealth = MaxHealth;
            InitializeTimers();
        }

        public void SpawnMe()
        {
            Sprite = new ColorSprite(Color.Black);
            CanShoot = true;
            Position = new Vector2(GameConstants.WINDOW_WIDTH / 4, GameConstants.WINDOW_HEIGHT / 2);
            CurrentHealth = MaxHealth;
            InitializeTimers();
        }

        public void LoseHealth(int amount)
        {
            CurrentHealth-=amount;
            Sprite = new ColorSprite( Color.Red);
            damageTakenTimer.Start();

            if (CurrentHealth <= 0)
            {
                GameManager.Instance.PlayerWon(this);
            }
        }

        public void SetVelocity(Vector2 direction)
        {
            Velocity = direction * CurrentSpeed;
        }

        public Bullet Shoot(Vector2 direction)
        {
            return new Bullet(Position, direction, EntityManager.Tank.PlayerId, CurrentDamage);
        }

        private void InitializeTimers()
        {
            shootingTimer = new Timer(1000 * shootingCooldown);
            shootingTimer.Elapsed += ShootingCooldownElapsed;

            damageTakenTimer = new Timer(100);
            damageTakenTimer.Elapsed += ChangeColorBackToNormal;
        }

        private void ChangeColorBackToNormal(object sender, ElapsedEventArgs e)
        {
            Sprite = new ColorSprite(Color.Black);
        }

        private void ShootingCooldownElapsed(object sender, ElapsedEventArgs e)
        {
            ToggleCanShoot();
            ((Timer)sender).Stop();
        }

        public void StartTimer()
        {
            shootingTimer.Start();
            ToggleCanShoot();
        }
        private void ToggleCanShoot()
        {
            CanShoot = !CanShoot;
        }
        public override void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 nextPosition = Position + Velocity;
            if (nextPosition.X < 0 || nextPosition.X + Width > GameConstants.WINDOW_WIDTH)
            {
                Velocity = new Vector2(0, Velocity.Y);
            }

            if (nextPosition.Y < 0 || nextPosition.Y + Height > GameConstants.WINDOW_HEIGHT)
            {
                Velocity = new Vector2(Velocity.X, 0);
            }
            Position += Velocity;
            CollisionShape.Position = Position;
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

        public void ApplyPowerup(PowerupEffect powerup)
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
                    CurrentDamage = bulletPowerup.damage;
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
