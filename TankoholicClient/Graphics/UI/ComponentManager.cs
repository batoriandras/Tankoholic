using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankoholicClient.Graphics.UI
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

        private SpriteFont spriteFont;
        private Texture2D heartSprite;

        private readonly List<Label> labels = new();
        private readonly List<Button> buttons = new();

        private readonly List<Image> healthBarImages = new();


        int myScore = 0;
        int enemyScore = 0;
        Label myScoreLabel;
        Label enemyScoreLabel;
        Label blocksRemainingLabel;

        Button disconnectButton;

        public void Initialize(ContentManager content)
        {
            myScoreLabel = new Label("My score: 0",
               new Vector2(10,10), Color.Black);
            labels.Add(myScoreLabel);

            enemyScoreLabel = new Label("Enemy's score: 0",
             new Vector2(GameConstants.WINDOW_WIDTH-270, 10), Color.Black);
            labels.Add(myScoreLabel);

            blocksRemainingLabel = new Label("Blocks: 10",
               new Vector2(10, GameConstants.WINDOW_HEIGHT), Color.Black);
            labels.Add(blocksRemainingLabel);

            /*
            disconnectButton = new Button("Disconnect", () =>
            {
                // TODO: implement disconnection
                Main.exitGame = true;
            },
            new Vector2(GameConstants.WINDOW_WIDTH/2 - 125, 10), Color.Gray, width: 250, height: 60);
            buttons.Add(disconnectButton);
            */
        }



        public void IncrementMyScore()
        {
            myScore++;
            myScoreLabel.Text = "My score: " + myScore;
        }
        public void ResetMyScore()
        {
            myScore = 0;
            myScoreLabel.Text = "My score: " + myScore;
        }
        public void IncrementEnemyScore()
        {
            enemyScore++;
            enemyScoreLabel.Text = "Enemy's score: " + enemyScore;
        }
        public void ResetEnemyScore()
        {
            enemyScore = 0;
            enemyScoreLabel.Text = "Enemy's score: " + enemyScore;
        }



        public void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("Font");
            heartSprite = content.Load<Texture2D>("Heart");
        }

        public void Update(MouseState currentMouseState, MouseState lastMouseState)
        {
            CheckButtonOnClick(currentMouseState, lastMouseState);

            for (int i =0; i< GameManager.Instance.Player.Tank.CurrentHealth; i++)
            {

            }
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

        public void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            foreach(Button button in buttons)
            {
                button.Draw(ref spriteBatch, ref rectangleBlock, ref spriteFont);
            }

            foreach (Label label in labels)
            {
                label.Draw(ref spriteBatch, ref rectangleBlock, ref spriteFont);
            }

            foreach (Image image in healthBarImages)
            {
                image.Draw(ref spriteBatch, ref rectangleBlock);
            }
        }       
    }
}
