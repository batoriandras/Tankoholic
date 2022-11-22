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
        List<TankSprite> otherTanks = new List<TankSprite>();

        public Player player = new Player("Én", 1);

        #region Map

        private readonly SpriteITile[,] map = new SpriteITile[GameConstants.CELLS_HORIZONTALLY_COUNT, GameConstants.CELLS_VERTICALLY_COUNT];

        public void GenerateField()
        {
            for (int y = 0; y < GameConstants.CELLS_VERTICALLY_COUNT; y++)
            {
                for (int x = 0; x < GameConstants.CELLS_HORIZONTALLY_COUNT; x++)
                {
                    map[x, y] = new SpriteGrassTile(new Vector2(x, y));
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
        }


        public void Draw(SpriteBatch spriteBatch, Texture2D rectangleBlock)
        {
            for (int y = 0; y < GameConstants.CELLS_VERTICALLY_COUNT; y++)
            {
                for (int x = 0; x < GameConstants.CELLS_HORIZONTALLY_COUNT; x++)
                {
                    map[x, y].Draw(ref spriteBatch, ref rectangleBlock);
                }
            }
        }

    }
}
