using Game.Units.Effects.Teporary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Units.Effects
{
    class Buffs
    {

        private Hero hero;
        private List<Effect> effects;

        public Buffs(Hero h)
        {
            effects = new List<Effect>();
            hero = h;
        }

        public void Add(Effect e)
        {
            effects.Add(e);
            e.SetHero(hero);
        }

        public void Remove(Effect e)
        {
            effects.Remove(e);
        }

        public int Count
        {
            get
            {
                return effects.Count;
            }
        }

        public Effect this[int i]
        {
            get
            {
                return effects[i];
            }
        }

        public List<TemporaryEffect> Temporary
        {
            get
            {
                List<TemporaryEffect> r = new List<TemporaryEffect>();
                for (int i = 0; i < effects.Count; i++)
                {
                    if (effects[i] is TemporaryEffect)
                    {
                        r.Add((TemporaryEffect)effects[i]);
                    }
                }
                return r;
            }
        }
    }
}
