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

        public bool CheckCollision(ICollisionShape collisionShape1, ICollisionShape collisionShape2)
        {
            if(collisionShape1 is CollisionCircle && collisionShape2 is CollisionCircle)
            {
                return CollideCircles((CollisionCircle)collisionShape1, (CollisionCircle)collisionShape2);
            }
            if(collisionShape1 is CollisionCircle && collisionShape2 is CollisionRectangle)
            {
                return CollideCircleWithRectangle((CollisionCircle)collisionShape1, (CollisionRectangle)collisionShape2);
            }
            if(collisionShape1 is CollisionRectangle && collisionShape2 is CollisionCircle)
            {
                return CollideCircleWithRectangle((CollisionCircle)collisionShape2, (CollisionRectangle)collisionShape1);
            }
            return false;
        }

        private bool CollideCircleWithRectangle(CollisionCircle c1, CollisionRectangle r2)
        {
            throw new NotImplementedException();
        }

        private bool CollideCircles(CollisionCircle c1, CollisionCircle c2)
        {
            Vector2 centerDistance = c1.CenterPosition - c2.CenterPosition;
            float centerDistanceSq = (float)(Math.Pow(centerDistance.X, 2) + Math.Pow(centerDistance.Y, 2));
            float radiusSq = (float)Math.Pow(c1.Radius + c2.Radius, 2);
            return centerDistanceSq <= radiusSq;
        }
        public void ResolveCollision(Entity entity1, Entity entity2)
        {
            if(CheckCollision(entity1.collisionShape, entity2.collisionShape))
            {
                if(entity1 is Tank && entity2 is Tank)
                {
                    ResolveCircle(entity1, entity2);
                }
            }
            
        }
        private void ResolveCircle(Entity entity1, Entity entity2)
        {
            Vector2 centerDistance = ((CollisionCircle)entity1.collisionShape).CenterPosition - ((CollisionCircle)entity2.collisionShape).CenterPosition;
            float radius_sum = ((CollisionCircle)entity1.collisionShape).Radius + ((CollisionCircle)entity2.collisionShape).Radius;
            float length = (float)Math.Sqrt(Math.Pow(centerDistance.X, 2) + Math.Pow(centerDistance.Y, 2));
            if(length == 0)
            {
                length = 1;
            }
            Vector2 unit = new Vector2(centerDistance.X / length, centerDistance.Y / length);
            float unitX = centerDistance.X / length;
            float unitY = centerDistance.Y / length;
            entity1.Position = entity2.position + (unit * (radius_sum + 1));
            entity1.collisionShape.Position = entity1.position;
        }
    }
}
