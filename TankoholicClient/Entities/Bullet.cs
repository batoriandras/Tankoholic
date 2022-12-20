using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TankoholicClient.Collision;

namespace TankoholicClient.Entities
{
    public class Bullet:Entity
    {
        private readonly Vector2 velocity;
        private const int MULTIPLIER = 10;
        public int Damage { get; set; }
        public Vector2 Direction { get; private set; }
        public int PlayerId { get; private set; }
        public Bullet(Vector2 position, Vector2 direction, int playerId, int damage = 5)
        {
            Width = 20;
            Height = 20;
            Position = position;
            Direction = direction;
            velocity = direction * MULTIPLIER;
            this.Damage = damage;

            CollisionShape = new CollisionCircle(position, (float)Width / 2);
            PlayerId = playerId;
        }


        public override void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 nextPosition = Position + velocity;
            if (nextPosition.X + Width >= 0 && nextPosition.X  <= GameConstants.WINDOW_WIDTH &&
                nextPosition.Y + Height >= 0 && nextPosition.Y <= GameConstants.WINDOW_HEIGHT)
            {
                Position += velocity;
            }
            else
            {
                EntityManager.EntityTrashcan.Add(this);
            }
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            spriteBatch.Draw(rectangleBlock,
                new Rectangle((int)Position.X, (int)Position.Y,
                    Width, Height),
                Color.Gray);
        }
    }
}
