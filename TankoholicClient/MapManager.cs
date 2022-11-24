using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TankoholicClient
{
    // Manages the game's rules
    public sealed class MapManager
    {

        #region Map

        private readonly ITile[,] map = new ITile[GameConstants.CELLS_HORIZONTALLY_COUNT, GameConstants.CELLS_VERTICALLY_COUNT];

        public void SetTile(int x, int y, ITile tile)
        {
            map[x, y] = tile;
        }

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
        private static MapManager instance = null;

        private MapManager() { }

        public static MapManager Instance
        {
            get
            {
                instance ??= new MapManager();
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
            for (int y = 0; y < GameConstants.CELLS_VERTICALLY_COUNT; y++)
            {
                for (int x = 0; x < GameConstants.CELLS_HORIZONTALLY_COUNT; x++)
                {
                    map[x, y].Update();
                }
            }
        }


        public void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
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
        }

        /*
        public void CheckIfButtonWasClicked(MouseState currentMouseState)
        {
            foreach (Button button in components)
            {
                if (currentMouseState.X >= button.Position.X
                && currentMouseState.X <= button.Position.X + button.Width
                && currentMouseState.Y >= button.Position.Y
                && currentMouseState.Y <= button.Position.Y + button.Height)
                {
                    button.OnClick();
                }
            }
        }
        */

        public void DrawWithPencilAt(int x, int y, Pencil pencil)
        {
            int size = 4;
            for (int py = -size; py <= size; py++)
            {
                for (int px = -size; px <= size; px++)
                {
                    if (y + py >= 0 && x + px >= 0
                    && y + py < GameConstants.CELLS_VERTICALLY_COUNT
                    && x + px < GameConstants.CELLS_HORIZONTALLY_COUNT)
                    {
                        SetTile(x + px, y + py, DrawnTile.FromPencil(pencil));
                    }
                }
            }
        }

        public void CheckIfCanDraw(MouseState currentMouseState)
        {

            for (int y = 0; y < GameConstants.CELLS_VERTICALLY_COUNT; y++)
            {
                for (int x = 0; x < GameConstants.CELLS_VERTICALLY_COUNT; x++)
                {
                    if (currentMouseState.X >= x * GameConstants.CELL_SIZE
                    && currentMouseState.X <= x * GameConstants.CELL_SIZE + GameConstants.CELL_SIZE
                    && currentMouseState.Y >= y * GameConstants.CELL_SIZE
                    && currentMouseState.Y <= y * GameConstants.CELL_SIZE + GameConstants.CELL_SIZE)
                    {
                        DrawWithPencilAt(x, y, GameManager.Instance.player.pencil);
                    }
                }
            }

        }
    }
}
