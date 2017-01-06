using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game.Data.Form
{

    class ConsoleOutput
    {
        private static Dictionary<long, string> log = new Dictionary<long,string>();
        private static string[] battle = new string[5];

        private static bool[] changes = new bool[] { false, false };

        public static void Add(long currentTime, string text)
        {
            log.Add(currentTime, text);
            for (int i = 0; i < log.Count; i++)
            {
                if (log.Keys.ElementAt(i) + 5000 < currentTime)
                {
                    log.Remove(log.Keys.ElementAt(i));
                    i--;
                }
                else
                {
                    break;
                }
            }
            changes[0] = true;
        }

        public static void Update(string[] battle)
        {
            if (ConsoleOutput.battle != battle)
            {
                ConsoleOutput.battle = battle;
                changes[1] = true;
            }
        }

        public void Print()
        {

            if (changes[0] || changes[1])
            {
                Console.Clear();

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(battle[i]);
                }
                for (int i = log.Values.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine(log.Values.ElementAt(i));
                }
                changes[0] = false;
                changes[1] = false;
            }
        }
    }
}
