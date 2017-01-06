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
        private static string log = "{0}.txt";
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
#pragma warning disable CS0168 // Переменная "e" объявлена, но ни разу не использована.
            catch (ArgumentException e)
#pragma warning restore CS0168 // Переменная "e" объявлена, но ни разу не использована.
            {

            }
        }
    }
}
