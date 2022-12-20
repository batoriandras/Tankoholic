using System;
using System.Threading.Tasks;

namespace TankoholicClient.Powerups.Effects
{
    public abstract class PowerupEffectTemporary : PowerupEffect
    {
        public Action OnEnd;

        public int Duration => 10;

        public void Start()
        {
            Task.Delay(Duration * 1000).ContinueWith(task => {
                OnEnd();
            });
        }
    }
}
