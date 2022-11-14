using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Riptide;
using System;
using TankoholicClassLibrary;

namespace TankoholicClient
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private MouseState lastMouseState;


        Player player = new Player("Attila", 1);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ClientNetworkManager.Instance.Initialize();

            ClientNetworkManager.Instance.Connect();


            MessageSender.SendName();

            Console.WriteLine("asdasd");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            MouseState currentMouseState = Mouse.GetState();

            player.SetPosition(currentMouseState.X, currentMouseState.Y);

            MessageSender.AddPosition(player);

            lastMouseState = currentMouseState;


            MessageSender.SendAll();
            ClientNetworkManager.Instance.Update();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        
    }
}