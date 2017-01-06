
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Units.Splells.Magician.Damage;
using Game.Units.Effects;
using Game.Units.Effects.Teporary;

namespace Game.Units.Spells.Magitian.Damage
{
    class PeriodicDamageSpell : SpellDamage
    {
        Effect effect;

        public PeriodicDamageSpell(int targets, bool team, int power, object[] specialInfo) : base(targets, team, power)
        {
            if (power <0 )
            {
                effect = new HealingTempEffect(int.Parse((string)specialInfo[1]),3000,-int.Parse((string)specialInfo[0]),(string)specialInfo[2]);
            }
            else
            {
                effect = new DamageTempEffect(int.Parse((string)specialInfo[1]), 1000, int.Parse((string)specialInfo[0]), (string)specialInfo[2]);
            }
        }

        public override void Action(Team[] teams, Hero hero)
        {
            this.hero = hero;

            int s = enemyTeam ? (hero.GetSide() == 0 ? 1 : 0) : (hero.GetSide() == 0 ? 0 : 1);

            List<Hero> targets = SelectTargets(teams[s]);

            int realPower = CalculatePower();

            DealDamage(targets, realPower);

            Affects(targets);
        }

        protected void Affects(List<Hero> targets)
        {
            foreach(Hero h in targets)
            {
                h.stats.AddEffect(effect);
            }
        }

    }
}
