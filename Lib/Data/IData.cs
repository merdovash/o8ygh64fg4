using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Data
{
    interface IData
    {
        Team LoadPlayerTeam(string PlyerName, int[] place);
        Team LoadMonsterTeam(int dungeonID);
        void SavePlayersTeam(string PlayerName, Team t);
        string CreatingNewPlayer();
    }
}
