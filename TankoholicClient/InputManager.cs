using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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

            

            EntityManager.Tank.SetVelocity(direction);

            if (direction.Length() != 0)
            {
                

                MessageSender.SendPosition(EntityManager.Tank);
            }

           
        }

        MouseState lastMouseInput;

        public void ShootInput(KeyboardState keyboardInput, MouseState mouseInput)
        {
            if (EntityManager.Tank.CanShoot 
                && mouseInput.LeftButton == ButtonState.Pressed)
            {
                var mouseDirection = Vector2.Normalize(
                    mouseInput.Position.ToVector2() - EntityManager.Tank.Position
                );
                EntityManager.SpawnBullet(mouseDirection);
                EntityManager.Tank.StartTimer();
            }

            lastMouseInput = mouseInput;
        }
        public void MouseInput(MouseState mouseInput)
        {
            if (mouseInput.RightButton == ButtonState.Pressed)
            {
                MapManager.Instance.CheckIfCanDraw(mouseInput);
            }
        }
    }
}
