using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using TankoholicClassLibrary;
using System.Timers;
using Riptide;
using TankoholicClient.Collision;

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

            StartNewGame();
        }

        public static void StartNewGame()
        {
            GameManager.Instance.Player.Tank.SpawnMe();
            EntityManager.OtherTanks.ForEach(tank => tank.SpawnEnemy());

            ComponentManager.Instance.ResetEnemyScore();
            ComponentManager.Instance.ResetMyScore();
        }

        public void Update()
        {
            if (EntityManager.Tank is null)
            {
                return;
            }
            InputManager.Instance.KeyboardInput(Keyboard.GetState());
            InputManager.Instance.MouseInput(Mouse.GetState());
            
            InputManager.Instance.ShootInput(Keyboard.GetState(), Mouse.GetState());
            
            EntityManager.OtherTanks.ForEach(tank => tank.Update());

            MapManager.Instance.Update();
            EntityManager.Tank.Update();
            EntityManager.Bullets.ForEach(bullet => bullet.Update());
            EntityManager.Bullets.ForEach(MessageSender.SendPosition);

            ComponentManager.Instance.Update(Mouse.GetState(), lastMouseState);

            lastMouseState = Mouse.GetState();

            PowerupManager.Instance.Update();

            EntityManager.UnpassableTiles.ForEach(unpassableTile => CollisionManager.Instance.ResolveCollision(Player.Tank, unpassableTile));

            EntityManager.Bullets.ForEach(bullet => CollisionManager.Instance.ResolveCollision(bullet, EntityManager.Tank));

            PowerupManager.Instance.Powerups.ForEach(powerup => CollisionManager.Instance.ResolveCollision(Player.Tank, powerup));

            for (int i = 0; i < EntityManager.Bullets.Count; i++)
            {
                EntityManager.OtherTanks.ForEach(tank => CollisionManager.Instance.ResolveCollision(EntityManager.Bullets[i], tank));
            }
            EntityManager.OtherTanks.ForEach(tank => CollisionManager.Instance.ResolveCollision(EntityManager.Tank, tank));
            foreach (var bullet in EntityManager.Bullets)
            {
                EntityManager.UnpassableTiles.ForEach(unpassableTile => CollisionManager.Instance.ResolveCollision(bullet, unpassableTile));
            }
            /*
            if (Player.OtherPlayers.TryGetValue(ClientNetworkManager.Instance.Client.Id, out Player localPlayer))
            {
                localPlayer.Update(Keyboard.GetState());
            }
            */
            EntityManager.ClearEntityTrashcan();
        }

        public void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            MapManager.Instance.Draw(ref spriteBatch, ref rectangleBlock);

            if (EntityManager.Tank is null)
            {
                return;
            }
            EntityManager.Tank.Draw(ref spriteBatch, ref rectangleBlock);
            /* Ideiglenesen tesztelésre */
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
