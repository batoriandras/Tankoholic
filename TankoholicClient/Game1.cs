using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Riptide;
using System;
using System.Diagnostics;

namespace TankoholicClient
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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


            SendName();

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


            ClientNetworkManager.Instance.Update();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        public void SendName()
        {
            // MessageSendMode.Reliable az TCP protokolt használ, ami azt jelenti hogy biztosra fog menni hogy az üzenet átment-e. Ez lassabb
            // MessageSendMode.Unreliable az UDP protokolt használ, amit nem érdekli hogy az üzenet megérkezett-e. Ez gyorsabb

            Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.name);
            message.AddString("Pista");
            ClientNetworkManager.Instance.Client.Send(message);
        }
    }
}