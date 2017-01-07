using Game.Data.Form;
using Game.Lib.Units;
using Game.Loggers;
using Game.Units.Heroes;
using Game.Units.Splells;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace Game.Units
{
    internal class Hero
    {
        public Stats stats;

        

        private bool side;

        private int id;
        public int ID
        {
            get
            {
                return id;
            }
        }
        public string Name;

        //Preparing hero
        public Hero(int id, string name, int health, int damage,  int def, int AS, int[] s, double[] i)
        {
            this.id = id;
            Name = name;
            spells = new List<Spell>();
            stats = new Stats( new int[] { damage, def, health },AS, s, i, this);
        }

        public void AddSpell(Spell spell)
        {
            spells.Add(spell);
        }

        public void SetSide(bool side)
        {
            this.side = side;
        }

        public Stopwatch timer;

        public ConsoleOutput co;

        //battle
        public void UpdateStatus(Team[] teams)
        {
            if (stats.AttackReady())
            {
                AutoAttack(teams);
            }
            stats.Update();
        }

        //autoattack
        public Hero SelectTarget(Team[] teams)
        {
            int n = 0;
            while (teams[side ? 0 : 1][n].stats.CurrentHealth <= 0)
            {
                n++;
                if (n >= teams[side ? 0 : 1].Count) n = 0;
            }
            return teams[side ? 0 : 1][n];
        }

        public void AutoAttack(Team[] teams)
        {
            Hero target;
            if (!stats.isDead)
            {
                target = SelectTarget(teams);
                int realDamage = (stats.Damage - target.stats.Def);
                realDamage = realDamage >= 1 ? realDamage : 1;
                co.Add(timer.ElapsedMilliseconds, (string.Format("Hero {0} deals {1} {3} to Hero {2}", Name, realDamage > 0 ? realDamage : -realDamage, target.Name, realDamage > 0 ? "damage" : "heal")));
                target.Damage(realDamage, DamageType.Physical);
                stats.AddSP(0.2);
            }
            stats.Attack();
        }

        //spells
        private List<Spell> spells;
        public void UseSpell(Team[] teams, long currentTime)
        {
            if (spells.Count != 0)
            {

                spells[0].Action(teams, this);
            }
        }

        //get damage;
        public int Damage(int damage, int type)
        {
            switch (type)
            {
                case 0:
                    {
                        int d = damage - stats.Def;
                        stats.HealthAffect(d);
                        if (damage > 0) stats.AddSP(0.1);
                        return d;
                    }
                case 1:
                    {
                        int d = damage - stats.MagicDef;
                        stats.HealthAffect(d);
                        if (damage > 0) stats.AddSP(0.1);
                        return d;
                    }
                case 2:
                    {
                        stats.HealthAffect(damage);
                        return damage;
                    }

                default:
                    {
                        stats.HealthAffect(damage);
                        if (damage > 0) stats.AddSP(0.1);
                        return damage;
                    } 
            }
        }

        //getters
        public int GetSide()
        {
            return side ? 1 : 0;
        }
    }
}