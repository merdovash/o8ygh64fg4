using Game.Dungeons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Loggers;
using Game.Data;

namespace Game.GamePlay
{
    class DungeonMenu : Menu
    {
        Team playersTeam;

        string PlayerName;

        public DungeonMenu(Team playersTeam, string PlayerName)
        {
            this.playersTeam = playersTeam;
            this.PlayerName = PlayerName;
        }

        public override void ShowMenu()
        {
            bool exit = false;
            Dungeon d;
            while (!exit)
            {
                Console.Write("Enter dungeon number :");
                try
                {
                    int answer = int.Parse(Console.ReadLine());
                    d = new Dungeon(answer);
                    d.Start(playersTeam);
                    Reader.SavePlayersTeam(PlayerName, playersTeam);
                }
#pragma warning disable CS0168 // Переменная "e" объявлена, но ни разу не использована.
                catch (Exception e)
#pragma warning restore CS0168 // Переменная "e" объявлена, но ни разу не использована.
                {
                    exit = true;
                }
               
            }
        }
    }
}