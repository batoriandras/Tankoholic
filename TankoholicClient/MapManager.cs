using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TankoholicClient.Graphics.Tiles;

namespace TankoholicClient
{
    public sealed class MapManager
    {

        private readonly Tile[,] map = new Tile[GameConstants.CELLS_HORIZONTALLY_COUNT, GameConstants.CELLS_VERTICALLY_COUNT];

        public void SetTile(int x, int y, Tile tile)
        {
            map[x, y] = tile;
            if (tile is UnpassableTile t)
            {
                EntityManager.UnpassableTiles.Add(t);
            }
        }

        private void GenerateField()
        {
            for (int y = 0; y < GameConstants.CELLS_VERTICALLY_COUNT; y++)
            {
                for (int x = 0; x < GameConstants.CELLS_HORIZONTALLY_COUNT; x++)
                {
                    map[x, y] = new GrassTile(new Vector2(x * GameConstants.CELL_SIZE, y * GameConstants.CELL_SIZE));
                }
            }
        }

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
                    
                    else if (map[x, y] is UnpassableTile)
                    {
                        (map[x, y] as UnpassableTile).Draw(ref spriteBatch, ref rectangleBlock);
                    }

                    else
                    {
                        map[x, y].Draw(ref spriteBatch, ref rectangleBlock);
                    }
                }
            }
        }

        public List<PassableTile> EmptyTiles()
        {
            List<PassableTile> tiles = new List<PassableTile>();
            for (int y = 0; y < GameConstants.CELLS_VERTICALLY_COUNT; y++)
            {
                for (int x = 0; x < GameConstants.CELLS_HORIZONTALLY_COUNT; x++)
                {
                    if (map[x,y] is PassableTile passableTile)
                    {
                        tiles.Add(passableTile);
                    }
                }
            }
            return tiles;
        }

        public void CheckIfCanDraw(MouseState currentMouseState)
        {
            for (int y = 0; y < GameConstants.CELLS_VERTICALLY_COUNT; y++)
            {
                for (int x = 0; x < GameConstants.CELLS_HORIZONTALLY_COUNT; x++)
                {
                    if (currentMouseState.X >= x * GameConstants.CELL_SIZE
                    && currentMouseState.X <= x * GameConstants.CELL_SIZE + GameConstants.CELL_SIZE
                    && currentMouseState.Y >= y * GameConstants.CELL_SIZE
                    && currentMouseState.Y <= y * GameConstants.CELL_SIZE + GameConstants.CELL_SIZE)
                    {
                        if (map[x,y] is GrassTile)
                        {
                            MessageSender.SendUnpassableTileSpawn(x, y);
                        }
                    }
                }
            }
        }

        public void RemoveDrawnTile(DrawnTile drawnTile)
        {
            Tuple<int, int> position = drawnTile.GetCellPosition();
            SetTile(position.Item1, position.Item2, 
                new GrassTile(
                    new Vector2(
                        position.Item1 * GameConstants.CELL_SIZE, 
                        position.Item2 * GameConstants.CELL_SIZE
                        )
                    )
                );
            MessageSender.SendUnpassableTileDespawn(position.Item1, position.Item2);
        }
    }
}
