using Game.Units;
using System;
using System.Collections.Generic;

namespace Game
{
    internal class Team
    {

        //main part
        private List<Hero> team;
        bool side;
        public Team(bool i)
        {
            team = new List<Hero>();
            side = i;
        }

        public void Prepare()
        {
            team.ForEach(x => x.stats.Prepare());
            UpdateStatus();
        }


        public void AddHero(Hero hero)
        {
            hero.SetSide(side);
            hero.stats.Attack((2 * team.Count + (side ? 1 : 0))*500-hero.stats.AttackSpeed);
            team.Add(hero);
            
        }

        //alive heroes
        private List<Hero> alive;
        public List<Hero> AliveHeroes
        {
            get
            {
                return alive;
            }
        }

        private int n = 0;

        public Hero GetNextHero()
        {
            Hero r = null;
            while (r == null)
            {
                if (team[n].stats.CurrentHealth > 0)
                {
                    r = team[n];
                }
                n++;
                if (n >= team.Count)
                {
                    n=0;
                }
            }
            return r;
        }

        public Hero this[int i]
        {
            get
            {
                return i<team.Count?team[i]:null;
            }
        }

        public int Count
        {
            get
            {
                return team.Count;
            }
        }

        public void UpdateStatus()
        {
            alive = team.FindAll(x=>!x.stats.isDead);
        }

        public bool isDead()
        {
            for (int i = 0; i < team.Count; i++)
            {
                if (!team[i].stats.isDead)
                {
                    return false;
                }
            }
            return true;
        }
    }
}