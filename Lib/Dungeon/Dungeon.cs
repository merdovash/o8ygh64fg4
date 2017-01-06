using Game.Data;
using Game.Loggers;
using Game.Units.Splells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Dungeons
{
    class Dungeon
    {
        private Battle battle;
        int id;
        private Team monsters;


        public Dungeon(int id)
        {
            this.id = id;

            LoadMonsters();
        }

        public void Start(Team playerTeam)
        {
            playerTeam.Prepare();

            battle = new Battle(playerTeam, monsters);

            battle.Start();
        }

        private void LoadMonsters()
        {
            monsters = Reader.LoadMonsterTeam(id);
            monsters.Prepare();
        }

    }
}
