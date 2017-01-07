using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Lib.Units
{
    interface ITimable
    {
        void SetTimer(Stopwatch s);
    }
}
