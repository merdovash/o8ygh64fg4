using Game.Spells;
using Game.Units;
using Game.Units.Spells.Magician;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Units.Splells.Magician.Healing
{
    class SpellHeal : Spell
    {
        

        public SpellHeal(int targets, bool team, int power)
        {
            Init(targets, team, power);

        }


        public override void Action(Team[] teams, Hero hero)
        {
            this.hero = hero;

            int realPower = CalculatePower();

            List<Hero> targets = SelectTargets(teams[hero.GetSide()]);

            DealDamage(targets, realPower);
        }

        protected override int CalculatePower()
        {
            return power - hero.stats[3];
        }

        protected override void Init2()
        {
            type = DamageType.Heal;
        }

        protected override List<Hero> SelectTargets(Team team)
        {
            List<Hero> targets = new List<Hero>();

            List<Hero> alive = team.AliveHeroes;

            int max = nTargets < alive.Count ? nTargets : alive.Count;

            Hero h;

            for (int i = 0; i <max; i++)
            {
                h = alive[0];

                for (int j = 1; j < alive.Count; j++)
                {
                    if (!targets.Contains(alive[j])) {
                        if (h.stats.CurrentHealthPercent > alive[j].stats.CurrentHealthPercent)
                        {
                            h = alive[j];
                        }
                    }
                }
                targets.Add(h);
                alive.Remove(h);
            }
            return targets;
        }
    }
}

