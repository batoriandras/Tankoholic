using Microsoft.Xna.Framework;
using System;

namespace TankoholicClient.Collision
{
    public class CollisionManager
    {
        #region Singleton
        private static CollisionManager instance;

        private CollisionManager() { }

        public static CollisionManager Instance
        {
            get
            {
                instance ??= new CollisionManager();
                return instance;
            }
        }
        #endregion

        private bool CheckCollision(Entity entity1, Entity entity2)
        {
            if(entity1.CollisionShape is CollisionCircle && entity2.CollisionShape is CollisionCircle)
            {
                return CollideCircles(entity1, entity2);
            }
            if(entity1.CollisionShape is CollisionCircle && entity2.CollisionShape is CollisionRectangle)
            {
                return CollideCircleWithRectangle(entity1, entity2);
            }
            if(entity1.CollisionShape is CollisionRectangle && entity2.CollisionShape is CollisionCircle)
            {
                return CollideCircleWithRectangle(entity2, entity1);
            }
            return false;
        }

        private bool CollideCircleWithRectangle(Entity circleEntity, Entity rectangleEntity)
        {
            Vector2 distance = GetDistance(circleEntity, rectangleEntity);
            float x = Math.Abs(distance.X);
            float y = Math.Abs(distance.Y);
            if (x >= rectangleEntity.CollisionShape.Width / 2 + ((CollisionCircle)circleEntity.CollisionShape).Radius)
            {
                return false;
            }
            if (y >= rectangleEntity.CollisionShape.Height / 2 + ((CollisionCircle)circleEntity.CollisionShape).Radius)
            {
                return false;
            }

            if (x < rectangleEntity.CollisionShape.Width / 2 - 1)
            {
                return true;
            }

            if (y < rectangleEntity.CollisionShape.Height / 2 - 1)
            {
                return true;
            }

            Vector2 cornerDistance = new Vector2(x - rectangleEntity.CollisionShape.Width,
                y - rectangleEntity.CollisionShape.Height);
            float cornerDistanceLength = GetLenghtOfDistance(cornerDistance);
            return cornerDistanceLength <= ((CollisionCircle)circleEntity.CollisionShape).Radius;
        }
        private Vector2 GetDistance(Entity entity1, Entity entity2)
        {
            Vector2 entity1Center = entity1.CollisionShape.Position + new Vector2(entity1.CollisionShape.Width / 2, entity1.CollisionShape.Height / 2);
            Vector2 entity2Center = entity2.CollisionShape.Position + new Vector2(entity2.CollisionShape.Width / 2, entity2.CollisionShape.Height / 2);
            return entity1Center - entity2Center;
        }
        private float GetLenghtOfDistance(Vector2 distance)
        {
            float centerDistanceLength = (float)Math.Sqrt(Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2));
            return centerDistanceLength;
        }

        private bool CollideCircles(Entity entity1, Entity entity2)
        {
            float radiusSq = (float)Math.Pow(entity1.CollisionShape.Width / 2 + entity2.CollisionShape.Width / 2, 2);
            return GetLenghtOfDistance(GetDistance(entity1, entity2)) <= Math.Sqrt(radiusSq);
        }
        public void ResolveCollision(Entity entity1, Entity entity2)
        {
            if(CheckCollision(entity1, entity2))
            {
                if(entity1 is Tank && entity2 is Tank)
                {
                    ResolveCircle(entity1, entity2);
                }
                
                else if (entity1 is Tank tank1 && entity2 is DrawnTile tile)
                {
                    ResolveCircleWithRectangle(tank1, tile);
                }

                else if (entity1 is Bullet bullet && entity2 is Tank tank && bullet.PlayerId != tank.PlayerId)
                {
                    TankHitWithBullet(bullet, tank);
                }
                else if (entity1 is Bullet bullet1 && entity2 is DrawnTile drawnTile)
                {
                    BulletHitDrawnTile(bullet1, drawnTile);
                }
                else if (entity1 is Tank tank2 && entity2 is PowerupEntity powerup)
                {
                    tank2.ApplyPowerup(powerup.Effect);

                    PowerupManager.Instance.RemovePowerup(powerup.Id);
                   // BulletHitDrawnTile((Bullet)entity1, (DrawnTile)entity2);
                }
            }
            
        }

        private void BulletHitDrawnTile(Bullet entity1, DrawnTile entity2)
        {
            EntityManager.EntityTrashcan.Add(entity1);
            EntityManager.EntityTrashcan.Add(entity2);
        }

        private void MoveTankOutOfTile(Tank tank, Vector2 velocity)
        {
            tank.SetVelocity(velocity);
            tank.Update();
        }
        private void ResolveCircleWithRectangle(Entity circleEntity, Entity rectangleEntity)
        {
            Vector2 distance = GetDistance(circleEntity, rectangleEntity);
            switch (distance.X)
            {
                case <= 0 when distance.Y <= 0:
                    MoveTankOutOfTile((Tank)circleEntity, new Vector2(-1, -1));
                    return;
                case <= 0 when distance.Y > 0:
                    MoveTankOutOfTile((Tank)circleEntity, new Vector2(-1, 1));
                    return;
                case > 0 when distance.Y <= 0:
                    MoveTankOutOfTile((Tank)circleEntity, new Vector2(1, -1));
                    return;
                case > 0 when distance.Y > 0:
                    MoveTankOutOfTile((Tank)circleEntity, new Vector2(1, 1));
                    return;
            }
        }

        private void ResolveCircle(Entity entity1, Entity entity2)
        {
            float radiusSum = entity1.CollisionShape.Width / 2 + entity2.CollisionShape.Width / 2;
            Vector2 centerDistance = GetDistance(entity1, entity2);
            float length = GetLenghtOfDistance(centerDistance);
            if(length == 0)
            {
                length = 1;
            }
            Vector2 unit = new Vector2(centerDistance.X / length, centerDistance.Y / length);
            entity1.Position = entity2.Position + (unit * (radiusSum + 1));
        }

        private void TankHitWithBullet(Bullet bullet, Tank tank)
        {
            EntityManager.EntityTrashcan.Add(bullet);
            tank.LoseHealth(bullet.Damage);
        }
    }
}
