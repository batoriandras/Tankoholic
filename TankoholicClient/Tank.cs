using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Timer = System.Timers.Timer;

namespace TankoholicClient
{ 
    public class Tank : Entity
    {        
        private readonly int shootingCooldown;
        private readonly int speed;
        private int health;
        private Vector2 velocity;
        private Timer shootingTimer;
        private Timer damageTakenTimer;
        private Color color;
        public ushort PlayerId { get; private set; }
        public Tank(Vector2 position, ushort playerId)
        {
            color = Color.Black;
            speed = 2;
            Width = 40;
            Height = 40;
            CollisionShape = new CollisionCircle((float)Width / 2, position);
            CanShoot = true;
            Position = position;
            health = 4;
            PlayerId = playerId;
            shootingCooldown = 1;
            InitializeTimers();
        }
        public bool CanShoot { get; private set; }

        public void LoseHealth()
        {
            health--;
            color = Color.Red;
            damageTakenTimer.Start();
        }
        public void SetVelocity(Vector2 direction)
        {
            velocity = direction * speed;
        }

        public Bullet Shoot(Vector2 direction)
        {
            return new Bullet(Position, direction, EntityManager.Tank.PlayerId);
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
            color = Color.Black;
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
                color);
        }
    }


}
