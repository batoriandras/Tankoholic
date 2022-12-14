using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TankoholicClient.Collision;
using TankoholicClient.Entities;
using TankoholicClient.Graphics.UI;
using TankoholicClient.Powerups;

namespace TankoholicClient
{
    public sealed class GameManager
    {

       
        public Player Player { get; set; }

        private MouseState lastMouseState;

        #region Singleton
        private static GameManager instance = null;

        private GameManager() { }

        public static GameManager Instance
        {
            get
            {
                instance ??= new GameManager();
                return instance;
            }
        }
        #endregion

        public void Initialize()
        {
            MapManager.Instance.Initialize();
            PowerupManager.Instance.Initialize();
        }

        public void PlayerWon(Tank winnerTank)
        {
            if (winnerTank == Player.Tank)
            {
                ComponentManager.Instance.IncrementEnemyScore();
            }else
            {
                ComponentManager.Instance.IncrementMyScore();
            }

        }

        public void Update()
        {
            if (EntityManager.Tank is null)
            {
                return;
            }
            
            MapManager.Instance.Update();
            
            ManageInputs();
            
            ManageEntities();
            
            ComponentManager.Instance.Update(Mouse.GetState(), lastMouseState);
            lastMouseState = Mouse.GetState();
            
            PowerupManager.Instance.Update();
            
            ManageCollisions();
        }

        private void ManageCollisions()
        {
            EntityManager.UnpassableTiles.ForEach(unpassableTile => CollisionManager.Instance.ResolveCollision(Player.Tank, unpassableTile));
            EntityManager.Bullets.ForEach(bullet => CollisionManager.Instance.ResolveCollision(bullet, EntityManager.Tank));
            PowerupManager.Instance.Powerups.ForEach(powerup => CollisionManager.Instance.ResolveCollision(Player.Tank, powerup));
            foreach (var bullet in EntityManager.Bullets)
            {
                EntityManager.OtherTanks.ForEach(tank => CollisionManager.Instance.ResolveCollision(bullet, tank));
            }
            EntityManager.OtherTanks.ForEach(tank => CollisionManager.Instance.ResolveCollision(EntityManager.Tank, tank));
            foreach (var bullet in EntityManager.Bullets)
            {
                EntityManager.UnpassableTiles.ForEach(unpassableTile => CollisionManager.Instance.ResolveCollision(bullet, unpassableTile));
            }
            
            EntityManager.ClearEntityTrashcan();
        }
        
        private void ManageEntities()
        {
            EntityManager.OtherTanks.ForEach(tank => tank.Update());
            EntityManager.Tank.Update();
            EntityManager.Bullets.ForEach(bullet => bullet.Update());
            EntityManager.Bullets.ForEach(MessageSender.SendPosition);
        }

        private void ManageInputs()
        {
            InputManager.Instance.KeyboardInput(Keyboard.GetState());
            InputManager.Instance.MouseInput(Mouse.GetState());
            InputManager.Instance.ShootInput(Keyboard.GetState(), Mouse.GetState());
        }

        public void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            MapManager.Instance.Draw(ref spriteBatch, ref rectangleBlock);

            if (EntityManager.Tank is null)
            {
                return;
            }
            EntityManager.Tank.Draw(ref spriteBatch, ref rectangleBlock);
            foreach (var tank in EntityManager.OtherTanks)
            {
                spriteBatch.Draw(rectangleBlock,
                    new Rectangle((int)tank.Position.X, (int)tank.Position.Y,
                        40, 40), Color.Blue);
            }
            foreach (var item in EntityManager.Bullets)
            {
                item.Draw(ref spriteBatch, ref rectangleBlock);
            }

            PowerupManager.Instance.Draw(ref spriteBatch, ref rectangleBlock);
        }


    }
}
