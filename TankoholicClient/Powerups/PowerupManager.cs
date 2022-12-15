using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicClient
{
    public class PowerupManager
    {
        #region Singleton
        private static PowerupManager instance = null;

        private PowerupManager() { }

        public static PowerupManager Instance
        {
            get
            {
                instance ??= new PowerupManager();
                return instance;
            }
        }
        #endregion

        private int SpawnDuration { get; set; } = 10;

        private int LifespanDuration { get; set; } = 24;

        private List<PowerupEntity> powerups = new List<PowerupEntity>();

        public List<PowerupEntity> Powerups { get => powerups; }

        public void Initialize()
        {
            Spawn();
        }


        private void Spawn()
        {
            PowerupEntity powerup = PowerupEntity.RandomPowerup();
            powerups.Add(powerup);
            RemoveWhenTimesUp(powerup);
            Task.Delay(SpawnDuration * 1000).ContinueWith(task => {
                Spawn();
            });
        }

        private void RemoveWhenTimesUp(PowerupEntity powerup)
        {
            Task.Delay(LifespanDuration * 1000).ContinueWith(task => {
                powerups.Remove(powerup);
            });
        }


        public void RemovePowerup(int id)
        {
            EntityManager.EntityTrashcan.AddRange(powerups.Where(x => x.Id == id));
        }

        public void Update()
        {

        }

        public void Draw(ref SpriteBatch spriteBatch, ref Texture2D rectangleBlock)
        {
            foreach (PowerupEntity powerup in powerups)
            {
                powerup.Draw(ref spriteBatch, ref rectangleBlock);
            }
        }
    }
}
