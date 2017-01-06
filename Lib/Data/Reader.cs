using Game.Units;
using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Units.Splells;
using Game.Loggers;

namespace Game.Data
{
    class Reader
    {

        private static string connectionString = "Provider=Microsoft.JET.OLEDB.4.0;Data Source=C:/Games/log/DB.mdb; Persist Security Info=False;";
        public static Hero GetHero(int id)
        {
            Hero h;

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand("SELECT * FROM Heroes WHERE id="+id, conn);

                conn.Open();

                OleDbDataReader r = command.ExecuteReader();

                r.Read();

                /*
                int ID = (int)r["id"];
                string name = (string)r["HeroName"];
                int baseHP = (int)r["baseHP"];
                int baseDmg = (int)r["baseDmg"];
                int baseDef = (int)r["baseDef"];
                */

                h = new Hero
                    (
                    (int)r["id"],
                    (string)r["HeroName"],
                    (int)r["baseHP"],
                    (int)r["baseDmg"],
                    (int)r["baseDef"],
                    (int)r["baseAS"],
                    new int[] {
                        (int)r["baseStr"],
                        (int)r["baseDex"],
                        (int)r["baseInt"],
                        (int)r["class"] },
                    new double[] {
                        double.Parse(r["str++"].ToString()),
                        double.Parse(r["dex++"].ToString()),
                        double.Parse(r["int++"].ToString()) }
                    );
                if ((string)r["spell1Type"]!="no")
                {
                    object[] si = new object[] { };
                    try
                    {
                        si = ((string)r["spell1SpecialInfo"]).Split(',');
                    }
                    catch
                    {

                    }
                    h.AddSpell(Spell.GetSpell((string)r["spell1Type"], (int)r["spell1Targets"], (bool)r["spell1Team"], (int)r["spell1Power"], si));
                }
            }

            return h;
        }


        public static Team LoadPlayerTeam(string PlayerName, int[] place)
        {
            Team t = new Team(false);

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {

                conn.Open();

                for (int i = 0; i < place.Length; i++)
                {
                    OleDbCommand command = new OleDbCommand("SELECT * FROM "+PlayerName+" WHERE place=" + place[i]+";", conn);
                    OleDbDataReader r = null;

                    while (true)
                    {
                        try
                        {
                            r = command.ExecuteReader();

                            r.Read();

                            Hero hero = Reader.GetHero((int)r[1]);
                            hero.stats.SetLevel((int)r["lvl"], (int)r["exp"]);

                            t.AddHero(hero);
                            break;
                        }
                        catch (OleDbException e)
                        {
                            Console.Write("This Name doesn't exist. Enter Correct Name ");
                            PlayerName = Console.ReadLine();
                            r.Close();
                        }
                        catch (NullReferenceException e)   
                        {
                            Console.Write(e.StackTrace);
                        }
                    }
                }
            }

                return t;
        }

        public static Team LoadMonsterTeam(int dungeonID)
        {
            Team t = new Team(true);

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {

                conn.Open();
                for (int i = 1; i <= 5; i++)
                {
                    OleDbCommand command = new OleDbCommand("SELECT * FROM Dungeon WHERE place=" + i, conn);

                    OleDbDataReader r = command.ExecuteReader();

                    r.Read();

                    Hero hero = Reader.GetHero((int)r["HeroId"]);
                    hero.stats.SetLevel(dungeonID, 0);

                    t.AddHero(hero);

                }
            }

            return t;
        }

        public static void SavePlayersTeam(string PlayerName, Team t)
        {

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                for (int i = 0; i < t.Count; i++)
                {
                    OleDbCommand command = new OleDbCommand("UPDATE "+PlayerName+" SET lvl="+t[i].stats.Level+ ", exp=" + t[i].stats.CurrentExp + " WHERE HeroID="+t[i].ID, conn);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static string CreatingNewPlayer()
        {
            string PlayerName;
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                List<string> Names = new List<string>();

                var data = conn.GetSchema();
                System.Data.DataRowCollection rc = data.Rows;

                Console.WriteLine("Enter you name ");

                PlayerName = Console.ReadLine();

                while (true)
                {
                    try
                    {
                        CreateNewPlayer(PlayerName);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("This name is already exist");

                        Console.WriteLine("Enter you name ");

                        PlayerName = Console.ReadLine();
                    }
                }
            }
            return PlayerName;    
        }

        public static void CreateNewPlayer(string PlayerName)
        {


            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                    OleDbCommand cmd = new OleDbCommand("select * into " + PlayerName + " from NewPlayer",conn);

                    cmd.ExecuteNonQuery();
            }
        }
    }
}
