using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankoholicClient
{
    public abstract class PowerupEffectTemporary : PowerupEffect
    {
        public Action OnEnd;

        public int Duration { get => 10; }

        public void Start()
        {
            Task.Delay(Duration * 1000).ContinueWith(task => {
                OnEnd();
            });
        }
    }
}
