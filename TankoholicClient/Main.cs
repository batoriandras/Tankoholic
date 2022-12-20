using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TankoholicClient.Graphics.UI;

namespace TankoholicClient
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D rectangleBlock;


        public static readonly bool ExitGame = false;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = GameConstants.WINDOW_WIDTH,
                PreferredBackBufferHeight = GameConstants.WINDOW_HEIGHT
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Exiting += (s, e) => ClientNetworkManager.Instance.Stop();
        }

        protected override void Initialize()
        {
            Window.Title = GameConstants.TITLE;

            ClientNetworkManager.Instance.Initialize();
            ClientNetworkManager.Instance.Connect();
            
            GameManager.Instance.Initialize();
            ComponentManager.Instance.Initialize(Content);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            rectangleBlock = new Texture2D(GraphicsDevice, 1, 1);
            Color xnaColorBorder = new Color(255, 255, 255);
            rectangleBlock.SetData(new[] { xnaColorBorder });

            ComponentManager.Instance.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
            || ExitGame)
            {
                Exit();
            }

            GameManager.Instance.Update();

            MessageSender.SendAll();
            ClientNetworkManager.Instance.Update();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            GameManager.Instance.Draw(ref spriteBatch, ref rectangleBlock);
            ComponentManager.Instance.Draw(ref spriteBatch, ref rectangleBlock);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}