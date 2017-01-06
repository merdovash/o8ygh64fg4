using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Units.Effects
{
    abstract class Effect
    {
        protected int power;
        protected string name;

        protected Hero h;

        protected long start;

        public abstract void Update(long CurrentTime);
       
        public abstract string Description(string heroName);

        public void Start(long currentTime)
        {
            start = currentTime;
        }

        internal void SetHero(Hero hero)
        {
            h = hero;
        }
    }
}
