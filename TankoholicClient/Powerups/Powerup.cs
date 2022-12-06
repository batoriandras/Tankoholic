using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TankoholicClient.Powerup
{

    public abstract class PowerupEntity : Entity
    {
        PowerupEffect effect;
    }

    public abstract class PowerupEffect
    {
        
    }

    public abstract class PowerupEffectPermanent : PowerupEffect
    {

    }

    public abstract class PowerupEffectTemporary : PowerupEffect
    {
        
        public int totalsecs = 20 * 60;


        public Action OnEnd;


        public void Start()
        {
            var startTime = DateTime.Now;


            Task.Delay(10 * 1000).ContinueWith(task => {
                OnEnd();
            });

            /*
            var timer = new Timer() { Interval = 1000 };

            timer.Tick += (obj, args) =>
            {
                if (totalsecs <= 0)
                {
                    timer.Stop();

                    OnEnd();
                }
                else
                {
                    totalsecs--;
                }
            };
            timer.Start();
            */
        }
    }


    public class HealthPowerup : PowerupEffect
    {
        public int healthRegain = 2;


    }

    public class SpeedUpPowerup : PowerupEffectTemporary
    {
        

        public int speed = 3;



    }
}
