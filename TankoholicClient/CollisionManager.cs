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
            if(entity1.collisionShape == CollisionShape.Circle && entity2.collisionShape == CollisionShape.Circle)
            {
                return CollideCircles(entity1, entity2);
            }
            if(entity1.collisionShape == CollisionShape.Circle && entity2.collisionShape == CollisionShape.Rectangle)
            {
                return CollideCircleWithRectangle(entity1, entity2);
            }
            if(entity1.collisionShape == CollisionShape.Rectangle && entity2.collisionShape == CollisionShape.Circle)
            {
                return CollideCircleWithRectangle(entity2, entity1);
            }
            return false;
        }

        private bool CollideCircleWithRectangle(Entity circleEntitiy, Entity rectangleEntity)
        {
            throw new NotImplementedException();
        }
        private Vector2 GetCenterDistance(Entity entity1, Entity entity2)
        {
            Vector2 entity1Center = entity1.Position + new Vector2(entity1.Width / 2, entity1.Width / 2);
            Vector2 entity2Center = entity2.Position + new Vector2(entity2.Width / 2, entity2.Width / 2);
            return entity1Center - entity2Center;
        }
        private float GetLenghtOfCenterDistance(Vector2 centerDistance)
        {
            float centerDistanceLength = (float)Math.Sqrt(Math.Pow(centerDistance.X, 2) + Math.Pow(centerDistance.Y, 2));
            return centerDistanceLength;
        }

        private bool CollideCircles(Entity entity1, Entity entity2)
        {
            float radiusSq = (float)Math.Pow(entity1.Width / 2 + entity2.Width / 2, 2);
            return GetLenghtOfCenterDistance(GetCenterDistance(entity1, entity2)) <= Math.Sqrt(radiusSq);
        }
        public void ResolveCollision(Entity entity1, Entity entity2)
        {
            if(CheckCollision(entity1, entity2))
            {
                if(entity1 is Tank && entity2 is Tank)
                {
                    ResolveCircle(entity1, entity2);
                }

                if (entity1 is Bullet && entity2 is Tank && ((Bullet)entity1).PlayerId != ((Tank)entity2).PlayerId)
                {
                    TankHitWithBullet((Bullet)entity1, (Tank)entity2);
                }
            }
            
        }
        private void ResolveCircle(Entity entity1, Entity entity2)
        {
            float radius_sum = entity1.Width / 2 + entity2.Width / 2;
            Vector2 centerDistance = GetCenterDistance(entity1, entity2);
            float length = GetLenghtOfCenterDistance(centerDistance);
            if(length == 0)
            {
                length = 1;
            }
            Vector2 unit = new Vector2(centerDistance.X / length, centerDistance.Y / length);
            entity1.Position = entity2.Position + (unit * (radius_sum + 1));
        }

        private void TankHitWithBullet(Bullet bullet, Tank tank)
        {
            //GameManager.Bullets.Remove(bullet);
            tank.LoseHealth();
        }
    }
}
