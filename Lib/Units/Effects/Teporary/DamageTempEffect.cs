using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Units.Effects.Teporary
{
    class DamageTempEffect : TemporaryEffect
    {
        public DamageTempEffect(int count, int delay, int power, string name) : base(count, delay, power, name)
        {
        }

        public override string Description(string heroName)
        {
            return String.Format("Hero {0} gets {1} damage by {2}", heroName, power, name);
        }

        public override void Update(long CurrentTime)
        {
            
        }
    }
}
