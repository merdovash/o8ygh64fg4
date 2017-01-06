using Game.Data.Form;
using Game.Loggers;
using Game.Spells.Physicals;
using Game.Units;
using Game.Units.Spells.Magitian.Damage;
using Game.Units.Splells.Magician.Damage;
using Game.Units.Splells.Magician.Healing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Units.Splells
{
    delegate void spell(int targets, bool team, int power);

    abstract class Spell
    {
        protected long currentTime;
        //skill of this hero
        protected Hero hero;

        //type of skill
        protected int type;

        //init
        protected int nTargets;
        protected bool enemyTeam;
        protected int power;
        protected void Init(int targets, bool team, int power)
        {
            nTargets = targets;
            enemyTeam = team;
            this.power = power;
            Init2();
        }

        //static getter
        public static Spell GetSpell(string type, int targets, bool team, int power, object[] specialEffects)
        {
            switch (type)
            {
                case "heal":
                    {
                        Spell s = new SpellHeal(targets, team, power);
                        return s;
                        
                    }
                case "damage":
                    {
                        Spell s = new SpellDamage(targets, team, power);
                        return s;
                    }
                case "periodic":
                    {
                        Spell s = new PeriodicDamageSpell(targets, team, power, specialEffects);
                        return s;
                    }
                case "physical damage":
                    {
                        Spell s = new PhysicalSpell(targets, team, power);
                        return s;
                    }
                default:
                    {
                        return null;
                    }

            }
        }

        public abstract void Action(Team[] teams, Hero hero);
        protected abstract int CalculatePower();
        protected abstract List<Hero> SelectTargets(Team team);
        protected abstract void Init2();

        protected virtual void DealDamage(List<Hero> targets, int realPower)
        {
            foreach (Hero h in targets)
            {
                h.Damage(realPower, type);
                //ConsoleOutput.Add(currentTime, String.Format("Hero {0} deals {2} {3} by Skill to Hero {1}", hero.Name, h.Name, realPower>0?realPower:-realPower,realPower>0?"damage":"Heal"));
            }
        }

    }
}
