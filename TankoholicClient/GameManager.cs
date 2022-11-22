using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TankoholicClassLibrary;
using Microsoft.Xna.Framework.Input;

namespace TankoholicClient
{
    public sealed class GameManager
    {
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

        public void Update()
        {
            if (Player.OtherPlayers.TryGetValue(ClientNetworkManager.Instance.Client.Id, out Player localPlayer))
            {
                localPlayer.Update(Keyboard.GetState());
            }

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

            /* Ideiglenesen tesztelésre */
            foreach(var player in Player.OtherPlayers.Values)
            {
                spriteBatch.Draw(rectangleBlock,
                        new Rectangle((int)player.Tank.position.X * GameConstants.CELL_SIZE, (int)player.Tank.position.Y * GameConstants.CELL_SIZE,
                                      50, 50), Color.Gray);
            }
        }
    }
}
