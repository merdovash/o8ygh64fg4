﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Data
{
    interface IData
    {
        void Add(string s);
        void Update(string[] s);
        void Print();
    }
}
