using Game.Units.Splells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Units;

namespace Game.Units.Spells.Magician
{
    abstract class MagicSpell : Spell
    {

        protected override void Init2()
        {
            type = DamageType.Magic;
        }

        protected override int CalculatePower()
        {
            return (int)(power * (hero.stats.Class == 3 ? (1 + (double)hero.stats[3] / 150) : 1));
        }
    }
}
