using Game.Units.Splells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Units;

namespace Game.Spells.Physicals
{
    class PhysicalSpell : Spell
    {

        public PhysicalSpell(int targets, bool enemyTeam, int power)
        {
            Init(targets, enemyTeam, power);
        }

        protected override void Init2()
        {
            type = DamageType.Physical;
        }

        protected override int CalculatePower()
        {
            switch (hero.stats.Class)
            {
                case 1: return power + hero.stats[1];
                case 2: return (int)(power * ((double)hero.stats[2] / 250) + hero.stats[2]);
                default: return power;
            }
        }

        public override void Action(Team[] teams, Hero hero)
        {
            this.hero = hero;

            int s = enemyTeam ? (hero.GetSide() == 0 ? 1 : 0) : (hero.GetSide() == 0 ? 0 : 1); //targeting enemy team

            List<Hero> targets = SelectTargets(teams[s]);

            int realPower = CalculatePower();

            DealDamage(targets, realPower);
        }

        protected override List<Hero> SelectTargets(Team team)
        {
            List<Hero> targets = new List<Hero>();
            List<Hero> alive = team.AliveHeroes;

            int max = nTargets < alive.Count ? nTargets : alive.Count;
            for (int i = 0; i < max; i++)
            {
                targets.Add(alive[i]);
            }

            return targets;
        }
    }
}
