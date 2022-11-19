using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace TankoholicClient
{
    public sealed class ComponentManager
    {
        #region Singleton
        private static ComponentManager instance = null;

        private ComponentManager() { }

        public static ComponentManager Instance
        {
            get
            {
                instance ??= new ComponentManager();
                return instance;
            }
        }
        #endregion

        
        private readonly List<Component> components = new();

        public void Initialize(ContentManager content)
        {
            
        }

        public void Update()
        {
           
        }
       
        public void Draw(SpriteBatch spriteBatch, Texture2D rectangleBlock, SpriteFont spriteFont)
        {
            
        }


        public void CheckIfButtonWasClicked(MouseState currentMouseState)
        {
            foreach (Button button in components)
            {
                if(currentMouseState.X >= button.Position.X
                && currentMouseState.X <= button.Position.X + button.Width
                && currentMouseState.Y >= button.Position.Y
                && currentMouseState.Y <= button.Position.Y + button.Height)
                {
                    button.OnClick();
                }
            }
        }

        void DrawWithPencilAt(int x, int y, Pencil pencil)
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
                        GameManager.Instance.SetTile(x + px, y + py, );

                        switch (pencilSelected)
                        {
                            case PencilType.BUNNY:
                                GameManager.Instance.SetFieldCellAnimal(x + px, y + py, new Rabbit());
                                break;
                            case PencilType.FOX:
                                GameManager.Instance.SetFieldCellAnimal(x + px, y + py, new Fox());
                                break;
                            case PencilType.WALL:
                                GameManager.Instance.SetFieldCellMatter(x + px, y + py, new Wall());
                                break;
                            case PencilType.WATER:
                                GameManager.Instance.SetFieldCellMatter(x + px, y + py, new Water(GameConstants.MINT_WATER_DEPTH));
                                break;
                            case PencilType.GRASS:
                                GameManager.Instance.SetFieldCellMatter(x + px, y + py, new Grass());
                                break;
                        }
                    }
                }
            }
        }

        public void CheckIfCanDraw(MouseState currentMouseState)
        {
            if (pencilSelected != PencilType.NONE)
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
                            DrawWithPencilAt(x, y);
                        }
                    }
                }
            }
        }
    }
}
