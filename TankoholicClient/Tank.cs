using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TankoholicClient;

namespace TankoholicClient
{ 
    public class Tank : Entity
    {        
        private int shootingCooldown;
        private Timer shootingTimer;
        private Timer damageTakenTimer;
        private Color color;
        public ushort PlayerId { get; private set; }
        public Tank(Vector2 position, ushort playerId)
        {
            color = Color.Black;
            Width = 40;
            Height = 40;
            CollisionShape = new CollisionCircle(Width / 2, position);
            CanShoot = true;
            Position = position;
            Health = 4;
            PlayerId = playerId;
            shootingCooldown = 1;
            InitializeTimers();
        }

        public int Speed { get; private set; } = 2;

        public Vector2 Velocity { get; private set; }
        public int Health { get; private set; }
        public bool CanShoot { get; private set; }

        public void LoseHealth()
        {
            Health--;
            color = Color.Red;
            damageTakenTimer.Start();
        }
        public void SetVelocity(Vector2 direction)
        {
            Velocity = direction * Speed;
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
            if (CanShoot == true)
            {
                CanShoot = false;
            }
            else
            {
                CanShoot = true;
            }
        }
        public override void Update()
        {
            Position += Velocity;
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
