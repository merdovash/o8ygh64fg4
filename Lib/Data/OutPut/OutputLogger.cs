using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Data.OutPut
{
    class OutputLogger
    {
        private static string log = "C:/Games/log/{0}.txt";
        private static int n = 1;

        public static void NewLog()
        {
            n = int.Parse(File.ReadAllText("C:/Games/log/inc2.txt"));
            File.WriteAllText("C:/Games/log/inc2.txt", (n + 1).ToString());
        }

        public static void Write(String n)
        {
            try
            {
                File.AppendAllText(String.Format(log, n), n);
            }
            catch (ArgumentException e)
            {

            }
        }
    }
}
