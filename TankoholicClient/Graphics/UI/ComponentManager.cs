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


        
    }
}
