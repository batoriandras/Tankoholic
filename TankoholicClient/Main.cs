using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Riptide;
using System;
using TankoholicClassLibrary;

namespace TankoholicClient
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D rectangleBlock;
        private SpriteFont spriteFont;

        private MouseState lastMouseState;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = GameConstants.WINDOW_WIDTH,
                PreferredBackBufferHeight = GameConstants.WINDOW_HEIGHT
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.Title = GameConstants.TITLE;

            ClientNetworkManager.Instance.Initialize();
            ClientNetworkManager.Instance.Connect();

            GameManager.Instance.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            rectangleBlock = new Texture2D(GraphicsDevice, 1, 1);
            Color xnaColorBorder = new Color(255, 255, 255);
            rectangleBlock.SetData(new[] { xnaColorBorder });
          //  spriteFont = Content.Load<SpriteFont>("Fonts/Arial");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            InputManager.Instance.KeyboardInput(Keyboard.GetState());
            InputManager.Instance.MouseInput(Mouse.GetState());

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
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}