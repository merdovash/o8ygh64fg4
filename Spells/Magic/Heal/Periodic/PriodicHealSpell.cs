
using Game.Units;
using Game.Units.Effects;
using Game.Units.Effects.Teporary;
using Game.Units.Splells.Magician.Healing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Spells.Magic.Heal.Periodic
{
    class PriodicHealSpell : SpellHeal
    {
        Effect effect;

        public PriodicHealSpell(int targets, bool team, int power, object[] specialInfo) : base(targets, team, power)
        {
            effect = new HealingTempEffect((int)specialInfo[1], 1000, (int)specialInfo[0], (string)specialInfo[2]);
        }

        public override void Action(Team[] teams, Hero hero)
        {
            this.hero = hero;

            int s = enemyTeam ? (hero.GetSide() == 0 ? 1 : 0) : (hero.GetSide() == 0 ? 0 : 1);

            List<Hero> targets = SelectTargets(teams[s]);

            int realPower = CalculatePower();

            DealDamage(targets, realPower);

            Affect(targets);
        }

        protected void Affect(List<Hero> targets)
        {
            foreach(Hero h in targets)
            {
                h.stats.AddEffect(effect);
            }
        }
    }
}
