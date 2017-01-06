using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Loggers

{
    class Logger2
    {
        public void CreateNewLog()
        {
            int i = Increment();
            log = String.Format("C:/Games/log/{0}.txt", i);
        }

        private static string log;

        public void Write(String n)
        {
            using (StreamWriter fs = new StreamWriter(log,true))
            {
                fs.WriteLine(n);
            }

        }

        private int Increment()
        {
            int _return;

            StreamReader sr = new StreamReader("C:/Games/log/inc.txt");
            _return = int.Parse(sr.ReadLine());
            sr.Close();

            StreamWriter sw = new StreamWriter("C:/Games/log/inc.txt");
            _return += 1;
            sw.WriteLine(_return);
            sw.Close();

            return _return;
        }
    }
}
