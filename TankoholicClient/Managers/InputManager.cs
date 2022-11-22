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

        public void UserInput(KeyboardState keyboardInput)
        {
            
        }

    }
}
