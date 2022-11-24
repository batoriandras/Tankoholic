using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using TankoholicClassLibrary;

namespace TankoholicClient
{
    // Manages the game's rules
    public sealed class GameManager
    {
        List<Tank> otherTanks = new List<Tank>();

        public Player player = new Player("Me", 1);

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
            MessageSender.SendName(player);
            MapManager.Instance.Initialize();
        }

        // Update the fields according to the rules
        public void Update()
        {
            otherTanks.ForEach(tank => tank.Update());

            MapManager.Instance.Update();
            player.Tank.Update();
        }


        public void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            MapManager.Instance.Draw(ref spriteBatch, ref rectangleBlock);

            player.Tank.Draw(ref spriteBatch, ref rectangleBlock);
        }
    }
}
