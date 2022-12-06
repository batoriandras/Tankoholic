﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using TankoholicClassLibrary;
using System.Timers;

namespace TankoholicClient
{
    public sealed class GameManager
    {
        List<Tank> otherTanks = new List<Tank>();
        private List<Bullet> bullets = new List<Bullet>();
        private Timer timer;

        public Player player = new Player(1, "Me");


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
            timer = new Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
        }

        public void Update()
        {
            InputManager.Instance.KeyboardInput(Keyboard.GetState());
            InputManager.Instance.MouseInput(Mouse.GetState());
            if (player.Tank.CanShoot && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                timer.Start();
                player.Tank.ToggleCanShoot();
                Bullet bullet = InputManager.Instance.ShootInput(Keyboard.GetState(), Mouse.GetState());
                bullets.Add(bullet);
            }
            otherTanks.ForEach(tank => tank.Update());

            MapManager.Instance.Update();
            player.Tank.Update();
            bullets.ForEach(bullet => bullet.Update());

            ComponentManager.Instance.Update(Mouse.GetState(), lastMouseState);

            lastMouseState = Mouse.GetState();

            Player.OtherPlayers.Values.ToList().ForEach(otherPlayer => CollisionManager.Instance.ResolveCollision(player.Tank, otherPlayer.Tank));
            /*
            if (Player.OtherPlayers.TryGetValue(ClientNetworkManager.Instance.Client.Id, out Player localPlayer))
            {
                localPlayer.Update(Keyboard.GetState());
            }
            */
        }

        public void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            MapManager.Instance.Draw(ref spriteBatch, ref rectangleBlock);

            player.Tank.Draw(ref spriteBatch, ref rectangleBlock);
            /* Ideiglenesen tesztelésre */
            foreach (var player in Player.OtherPlayers.Values)
            {
                spriteBatch.Draw(rectangleBlock,
                    new Rectangle((int)player.Tank.Position.X, (int)player.Tank.Position.Y,
                        40, 40), Color.Blue);
            }
            foreach (var item in bullets)
            {
                item.Draw(ref spriteBatch, ref rectangleBlock);
            }
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            player.Tank.ToggleCanShoot();
            ((Timer)source).Stop();
        }
    }
}
