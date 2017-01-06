using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Units.Effects.Teporary
{
    abstract class TemporaryEffect :Effect
    {
        public int delay;
        public int tickCount;

        public TemporaryEffect(int count, int delay, int power, string name)
        {
            this.name = name;
            tickCount = count;
            this.delay = delay;
            this.power = power;
        }

        public int tick = 0;
        

        public bool isOver()
        {
            return tickCount <= tick;
        }
    }
}
