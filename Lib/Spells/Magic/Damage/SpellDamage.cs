using Game.Spells;
using Game.Units;
using Game.Units.Spells.Magician;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Units.Splells.Magician.Damage
{
    class SpellDamage : MagicSpell
    {
        public SpellDamage(int targets, bool team, int power)
        {
            Init(targets, team, power);
            
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
            List<Hero> alive  = team.AliveHeroes;

            int max = nTargets < alive.Count ? nTargets : alive.Count;
            for (int i = 0; i < max; i++)
            {
                targets.Add(alive[i]);
            }

            return targets;
        }

    }
}
