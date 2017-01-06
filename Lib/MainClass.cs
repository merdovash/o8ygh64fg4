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
            Team team1;
            team1 = Reader.Connecting();

            Menu m = new DungeonMenu( team1);
            m.ShowMenu();

            return 0;
        }
    }
}
