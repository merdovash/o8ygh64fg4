using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game.Loggers;
using Game.Units.Splells;
using Game.GamePlay;
using Game.Dungeons;
using Game.Data;

namespace Game
{
    class MainClass
    {
        static int Main(string[] args)
        {

            Console.WriteLine("Do you have account? y/n ");
            string s = Console.ReadLine();
            string playerName;
            Team team1;
            if (s == "y")
            {
                Console.Write("Enter you name");
                playerName = Console.ReadLine();
                team1 = Reader.LoadPlayerTeam(playerName, new int[] { 1, 2, 3, 4, 5 });
            }
            else
            {
                playerName = Reader.CreatingNewPlayer();
                team1 = Reader.LoadPlayerTeam(playerName, new int[] { 1, 2, 3, 4, 5 });
            }

            Menu m = new DungeonMenu( team1, playerName);
            m.ShowMenu();

            return 0;
        }
    }
}
