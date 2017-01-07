using Game.Data;
using Game.Data.Form;
using Game.Data.OutPut;
using Game.Loggers;
using Game.Units;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Battle
    {
        private Team[] teams;

        public Battle (Team team1, Team team2)
        {
            teams = new Team[] { team1, team2 };
        }


        Stopwatch timer;
        int n;

        ConsoleOutput co;

        public void Prepare()
        {
            co = new ConsoleOutput();

            timer = new Stopwatch();

            Array.ForEach(teams, x => x.Prepare());

            n = 1;

            Array.ForEach(teams, x => { x.SetTimer(timer); x.SetCout(co); });
        }

        public void Start()
        {
            
            timer.Start();

            PrintStatus();

            while (!(teams[0].isDead() || teams[1].isDead())) //пока обе команды живы
            {
                //обходим всех живых героев
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < teams[i].AliveHeroes.Count; j++)
                    {
                        teams[i].AliveHeroes[j].UpdateStatus(teams); 

                        teams[(n + 1) % 2].UpdateStatus();

                        PrintStatus();
                        n++;
                    }
                }

                Spells();
                co.Print();
            }

            co.Add(timer.ElapsedMilliseconds,teams[0].isDead() ? "Right win" : "Left win");

            if (!teams[0].isDead())
            {
                for (int i =0; i < teams[0].Count; i++)
                {
                    teams[0][i].stats.AddExp(50);
                }
                co.Add(timer.ElapsedMilliseconds, "You Win");
            }
            else
            {
                co.Add(timer.ElapsedMilliseconds,"you Lose");
            }
        }

        private void PrintStatus()
        {
            string[] l = new string[5];
            for (int i = 0; i < 5; i++)
            {
                //Console.WriteLine(String.Format("{0,7} : {1,7}:{4,4} {2,19} :  {3,3}:{5,4}", teams[0][i].Name, teams[0][i].stats.CurrentHealth, teams[1][i].Name, teams[1][i].stats.CurrentHealth, teams[0][i].stats.SP, teams[1][i].stats.SP));
                l[i]=String.Format("{0,7} : {1,7}:{4,4,4} {2,19} :  {3,3}:{5,4,4}", teams[0][i].Name, teams[0][i].stats.CurrentHealth, teams[1][i].Name, teams[1][i].stats.CurrentHealth, teams[0][i].stats.SP, teams[1][i].stats.SP);
            }
            co.Update(l);
        }

        private void Spells()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < teams[i].AliveHeroes.Count; j++)
                {
                        if (teams[i].AliveHeroes[j].stats.SpellReady)
                        {
                            teams[i].AliveHeroes[j].UseSpell(teams,timer.ElapsedMilliseconds);
                        }
                }
            }
        }
       
    }
}
