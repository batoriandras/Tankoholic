using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicClient
{
    public class CollisionManager
    {
        #region Singleton
        private static CollisionManager instance = null;

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

        public bool CheckCollision(Entity entity1, Entity entity2)
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

        private bool CollideCircleWithRectangle(Entity circleEntitiy, Entity rectangleEntity)
        {
            float x = Math.Abs(circleEntitiy.Position.X - rectangleEntity.Position.X + rectangleEntity.CollisionShape.Width / 2);
            float y = Math.Abs(circleEntitiy.Position.Y - rectangleEntity.Position.Y + rectangleEntity.CollisionShape.Height / 2);
            if (x > rectangleEntity.CollisionShape.Width / 2 + ((CollisionCircle)circleEntitiy.CollisionShape).Radius)
            {
                return false;
            }
            if (y > rectangleEntity.CollisionShape.Height / 2 + ((CollisionCircle)circleEntitiy.CollisionShape).Radius)
            {
                return false;
            }

            if (x <= rectangleEntity.CollisionShape.Width / 2)
            {
                return true;
            }

            if (y <= rectangleEntity.CollisionShape.Height / 2)
            {
                return true;
            }

            float cornerDistance_sq = (float)Math.Pow(x - rectangleEntity.Width / 2, 2) +
                                      (float)Math.Pow(y - rectangleEntity.Height / 2, 2);
            return cornerDistance_sq <= Math.Pow(((CollisionCircle)circleEntitiy.CollisionShape).Radius, 2);
        }

        private Vector2 GetLengthOfRectangleAndCircle(CollisionCircle cirlceShape, CollisionRectangle rectangleShape)
        {
            float nx = Math.Max(rectangleShape.Position.X,
                Math.Min(rectangleShape.Position.X + rectangleShape.Width,
                    cirlceShape.CenterPosition.X));
            float ny = Math.Max(rectangleShape.Position.Y,
                Math.Min(rectangleShape.Position.Y + rectangleShape.Height,
                    cirlceShape.CenterPosition.Y));
            return new Vector2(nx, ny);
        }
        private Vector2 GetCenterDistance(Entity entity1, Entity entity2)
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
            return GetLenghtOfDistance(GetCenterDistance(entity1, entity2)) <= Math.Sqrt(radiusSq);
        }
        public void ResolveCollision(Entity entity1, Entity entity2)
        {
            if(CheckCollision(entity1, entity2))
            {
                if(entity1 is Tank && entity2 is Tank)
                {
                    ResolveCircle(entity1, entity2);
                }
                
                else if (entity1 is Tank && entity2 is DrawnTile)
                {
                    ResolveCircleWithRectangle((Tank)entity1, (DrawnTile)entity2);
                    // ResolveCircle(entity1, entity2);
                }

                else if (entity1 is Bullet bullet && entity2 is Tank tank && bullet.PlayerId != tank.PlayerId)
                {
                    TankHitWithBullet(bullet, tank);
                }
            }
            
        }

        private void ResolveCircleWithRectangle(Entity circleEntitiy, Entity rectangleEntity)
        {
            if (circleEntitiy.CollisionShape.Position.X > rectangleEntity.CollisionShape.Position.X + rectangleEntity.CollisionShape.Width / 2)
            {
                circleEntitiy.Position += new Vector2(2, 0);
            }else 
            if (circleEntitiy.CollisionShape.Position.X < rectangleEntity.CollisionShape.Position.X)
            {
                circleEntitiy.Position -= new Vector2(2, 0);
            }

            if (circleEntitiy.CollisionShape.Position.Y > rectangleEntity.CollisionShape.Position.Y + rectangleEntity.CollisionShape.Height / 2)
            {
                circleEntitiy.Position += new Vector2(0, 2);
            }else
            if (circleEntitiy.CollisionShape.Position.Y < rectangleEntity.CollisionShape.Position.Y)
            {
                circleEntitiy.Position -= new Vector2(0, 2);
            }
        }

        private void ResolveCircle(Entity entity1, Entity entity2)
        {
            float radius_sum = entity1.CollisionShape.Width / 2 + entity2.CollisionShape.Width / 2;
            Vector2 centerDistance = GetCenterDistance(entity1, entity2);
            float length = GetLenghtOfDistance(centerDistance);
            if(length == 0)
            {
                length = 1;
            }
            Vector2 unit = new Vector2(centerDistance.X / length, centerDistance.Y / length);
            entity1.Position = entity2.Position + (unit * (radius_sum + 1));
        }

        private void TankHitWithBullet(Bullet bullet, Tank tank)
        {
            EntityManager.EntityTrashcan.Add(bullet);
            tank.LoseHealth();
        }
    }
}
