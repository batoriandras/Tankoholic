using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using TankoholicClassLibrary;
using TankoholicClient.Powerups;

namespace TankoholicClient
{
    public sealed class GameManager
    {
        List<Tank> otherTanks = new List<Tank>();

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
            PowerupManager.Instance.Initialize();
        }

        public void Update()
        {
            otherTanks.ForEach(tank => tank.Update());

            MapManager.Instance.Update();
            player.Tank.Update();

            ComponentManager.Instance.Update(Mouse.GetState(), lastMouseState);

            lastMouseState = Mouse.GetState();

            Player.OtherPlayers.Values.ToList().ForEach(otherPlayer => CollisionManager.Instance.ResolveCollision(player.Tank, otherPlayer.Tank));

            PowerupManager.Instance.Update();
        
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

            PowerupManager.Instance.Draw(ref spriteBatch, ref rectangleBlock);
        }
    }
}
