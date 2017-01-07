using Game.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Lib.Data.Form
{
    class Cout : IData
    {
        List<string> logT;
        List<long> logK;

        string[] text;

        bool[] changes;

        public Cout()
        {
            logT = new List<string>();
            logK = new List<long>();
            text = new string[5];
            changes = new bool[2];
        }

        public void Add(string s)
        {
            
        }

        public void Print()
        {
            
        }

        public void Update(string[] s)
        {
            
        }
    }
}
