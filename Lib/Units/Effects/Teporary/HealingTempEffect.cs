using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Units.Effects.Teporary
{
    class HealingTempEffect : TemporaryEffect
    {
        public HealingTempEffect(int count, int delay, int power, string name) : base(count, delay, power, name)
        {
        }

        public override string Description(string heroName)
        {
            return String.Format("Hero {0} gets {1} heal", heroName, power);
        }

        public override void Update(long currentTime)
        {
            if (start + delay * tick < currentTime)
            {
                h.Damage(-power, DamageType.Heal);
                tick++;
            }
        }
    }
}
