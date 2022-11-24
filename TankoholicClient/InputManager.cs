using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankoholicClassLibrary;

namespace TankoholicClient
{
    public class InputManager
    {
        #region Singleton
        private static InputManager instance = null;

        private InputManager() { }

        public static InputManager Instance
        {
            get
            {
                instance ??= new InputManager();
                return instance;
            }
        }
        #endregion

        public void KeyboardInput(KeyboardState keyboardInput)
        {
            Vector2 direction = new Vector2(0, 0);
            if (keyboardInput.IsKeyDown(Keys.W))
            {
                direction += new Vector2(0, -1);
            }
            if (keyboardInput.IsKeyDown(Keys.S))
            {
                direction += new Vector2(0, 1);
            }
            if (keyboardInput.IsKeyDown(Keys.A))
            {
                direction += new Vector2(-1, 0);
            }
            if (keyboardInput.IsKeyDown(Keys.D))
            {
                direction += new Vector2(1, 0);
            }

            GameManager.Instance.player.Tank.SetVelocity(direction);
        }

        public void MouseInput(MouseState mouseInput)
        {
            if (mouseInput.RightButton == ButtonState.Pressed)
            {
                MapManager.Instance.DrawWithPencilAt(mouseInput.X, mouseInput.Y, GameManager.Instance.player.pencil);
            }
        }
    }
}
