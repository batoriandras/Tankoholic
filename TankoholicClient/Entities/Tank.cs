using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TankoholicClient.Collision;
using TankoholicClient.Graphics.Sprites;
using TankoholicClient.Powerups.Effects;
using Timer = System.Timers.Timer;

namespace TankoholicClient.Entities
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

        private const float INITIAL_SPEED = 2.8f;
        private float currentSpeed = 2.8f;

        private const int INITIAL_DAMAGE = 1;
        private int currentDamage  = 1;

        private Vector2 velocity;

        private const int MAX_HEALTH = 4;
        private int currentHealth = 4;
        public int CurrentHealth
        {
            get => currentHealth;
            private set => currentHealth = value <= MAX_HEALTH ? value : MAX_HEALTH;
        }

        private PowerupEffect AppliedPowerup { get; set; }
        
        public bool CanShoot { get; private set; }

        public void Respawn()
        {
            Sprite = new ColorSprite(Color.Black);
            CanShoot = true;
            Position = new Vector2(GameConstants.WINDOW_WIDTH / 2, GameConstants.WINDOW_HEIGHT / 2);
            CurrentHealth = MAX_HEALTH;
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
            velocity = direction * currentSpeed;
        }

        public Bullet Shoot(Vector2 direction)
        {
            return new Bullet(Position, direction, EntityManager.Tank.PlayerId, currentDamage);
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
            Vector2 nextPosition = Position + velocity;
            if (nextPosition.X < 0 || nextPosition.X + Width > GameConstants.WINDOW_WIDTH)
            {
                velocity = new Vector2(0, velocity.Y);
            }

            if (nextPosition.Y < 0 || nextPosition.Y + Height > GameConstants.WINDOW_HEIGHT)
            {
                velocity = new Vector2(velocity.X, 0);
            }
            Position += velocity;
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
                    currentSpeed = speedUpPowerup.Speed;
                    Sprite = new ColorSprite(Color.Red);
                    speedUpPowerup.OnEnd = delegate ()
                    {
                        Sprite = new ColorSprite(Color.Black);
                        currentSpeed = INITIAL_SPEED;
                        DisposePowerup();
                    };
                }
                if (AppliedPowerup is BulletPowerup bulletPowerup)
                {
                    currentDamage = bulletPowerup.Damage;
                    Sprite = new ColorSprite(Color.Blue);
                    bulletPowerup.OnEnd = delegate ()
                    {
                        Sprite = new ColorSprite(Color.Black);
                        currentDamage = INITIAL_DAMAGE;
                        DisposePowerup();
                    };
                }
                tempPowerup.Start();
            }

            else if (AppliedPowerup is PowerupEffectPermanent permanentPowerup)
            {
                if (AppliedPowerup is HealthPowerup healthPowerup)
                {
                    CurrentHealth += healthPowerup.HealthRegain;
                    DisposePowerup();
                }
            }
        }
    }


}
