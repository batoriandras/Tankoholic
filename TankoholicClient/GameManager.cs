using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using TankoholicClassLibrary;
using TankoholicLibrary;

namespace TankoholicClient
{
    // Manages the game's rules
    public sealed class GameManager
    {
        List<Tank> otherTanks = new List<Tank>();

        public Player player = new Player("Me", 1);

        #region Map

        private readonly ITile[,] map = new ITile[GameConstants.CELLS_HORIZONTALLY_COUNT, GameConstants.CELLS_VERTICALLY_COUNT];

        public void GenerateField()
        {
            for (int y = 0; y < GameConstants.CELLS_VERTICALLY_COUNT; y++)
            {
                for (int x = 0; x < GameConstants.CELLS_HORIZONTALLY_COUNT; x++)
                {
                    map[x, y] = new GrassTile(new Vector2(x * GameConstants.CELL_SIZE, y * GameConstants.CELL_SIZE));
                }
            }
        }

        #endregion

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
            GenerateField();
        }

        // Update the fields according to the rules
        public void Update()
        {
            otherTanks.ForEach(tank => tank.Update());

            for (int y = 0; y < GameConstants.CELLS_VERTICALLY_COUNT; y++)
            {
                for (int x = 0; x < GameConstants.CELLS_HORIZONTALLY_COUNT; x++)
                {
                    map[x, y].Update();
                }
            }
            player.Tank.Update();
        }


        public void Draw(SpriteBatch spriteBatch, Texture2D rectangleBlock)
        {
            for (int y = 0; y < GameConstants.CELLS_VERTICALLY_COUNT; y++)
            {
                for (int x = 0; x < GameConstants.CELLS_HORIZONTALLY_COUNT; x++)
                {
                    if (map[x, y] is GrassTile)
                    {
                        (map[x, y] as GrassTile).Draw(ref spriteBatch, ref rectangleBlock);
                    }
                    else
                    {
                        map[x, y].Draw(ref spriteBatch, ref rectangleBlock);
                    }
                    
                }
            }

            player.Tank.Draw(ref spriteBatch, ref rectangleBlock);
        }
    }
}
