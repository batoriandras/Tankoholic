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


        private readonly List<Label> labels = new();
        private readonly List<Button> buttons = new();


        Label myScoreLabel;
        Label blocksRemainingLabel;

        Button disconnectButton;

        public void Initialize(ContentManager content)
        {
            myScoreLabel = new Label("My score: 0",
               new Vector2(10,10), Color.Black);
            labels.Add(myScoreLabel);

            blocksRemainingLabel = new Label("Blocks: 10",
               new Vector2(10, GameConstants.WINDOW_HEIGHT), Color.Black);
            labels.Add(blocksRemainingLabel);

            disconnectButton = new Button("Disconnect", () =>
            {
                // TODO: implement disconnection
                Main.exitGame = true;
            },
            new Vector2(GameConstants.WINDOW_WIDTH/2 - 125, 10), Color.Gray, width: 250, height: 60);
            buttons.Add(disconnectButton);
        }

        public void Update(MouseState currentMouseState, MouseState lastMouseState)
        {
            CheckButtonOnClick(currentMouseState, lastMouseState);
        }

        public void CheckButtonOnClick(MouseState currentMouseState, MouseState lastMouseState)
        {
            foreach (Button button in buttons)
            {
                if (lastMouseState.LeftButton == ButtonState.Pressed
                && currentMouseState.LeftButton == ButtonState.Released
                && currentMouseState.X >= button.Position.X
                && currentMouseState.X <= button.Position.X + button.Width
                && currentMouseState.Y >= button.Position.Y
                && currentMouseState.Y <= button.Position.Y + button.Height)
                {
                    button.OnClick();
                }
            }
        }

        public void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock, ref SpriteFont spriteFont)
        {
            foreach(Button button in buttons)
            {
                button.Draw(ref spriteBatch, ref rectangleBlock, ref spriteFont);
            }

            foreach (Label label in labels)
            {
                label.Draw(ref spriteBatch, ref rectangleBlock, ref spriteFont);
            }
        }    }
}
