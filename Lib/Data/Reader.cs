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


        public static Team LoadPlayerTeam(string username, int[] place, OleDbConnection conn)
        {
            Team t = new Team(false);

            for (int i = 0; i < place.Length; i++)
            {
                OleDbCommand command = new OleDbCommand("SELECT * FROM " + username + " WHERE place=" + place[i] + ";", conn);
                OleDbDataReader r = null;

                r = command.ExecuteReader();

                r.Read();

                Hero hero = GetHero((int)r[1]);
                hero.stats.SetLevel((int)r["lvl"], (int)r["exp"]);

                t.AddHero(hero);
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

        public static Team Connecting()
        {
            string username;
            string password;
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                while (true)
                {
                    Console.Write("Enter username : ");
                    username = Console.ReadLine();
                    OleDbCommand getPassword = null;
                    try
                    {
                        getPassword = new OleDbCommand(String.Format("SELECT * FROM users WHERE username='{0}'",username), conn); //пытаемся получить пароль юзера

                        OleDbDataReader dr = getPassword.ExecuteReader();
                        dr.Read();

                        Console.Write("Enter password :");
                        password = Console.ReadLine();

                        if (password == dr["userpassword"].ToString())
                        {
                            return LoadPlayerTeam(username,new int[] { 1, 2, 3, 4, 5 }, conn);
                        }
                    }
                    catch (OleDbException e)
                    {
                        Console.Write("this username doesn't exist. do you want to create new user? y/n  ");
                        if (Console.ReadLine() == "y")
                        {
                            CreateNewPlayer(username, conn);
                        }
                    }
                    
                }
            }
        }

        public static void CreateNewPlayer(String name, OleDbConnection conn)
        {
            string password;

            while(true)
            {
                if (Check(name, conn))
                {
                    Console.Write("enter you password : ");
                    password = Console.ReadLine();
                    break;
                }
                else
                {
                    Console.Write("This name is already exist. Enter new name: ");
                    name = Console.ReadLine();
                }
            }

            OleDbCommand cmd;
            cmd = new OleDbCommand(String.Format("INSERT INTO users (username, userpassword) VALUES ('{0}', '{1}')", name, password), conn);
            cmd.ExecuteNonQuery();

            cmd = new OleDbCommand("select * into " + name + " from NewPlayer", conn);
            cmd.ExecuteNonQuery();
        }

        public static bool Check(string name, OleDbConnection conn)
        {
            OleDbCommand cmd = new OleDbCommand("Select username FROM users", conn);

            OleDbDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                if (r["username"].ToString() == name) return false;
            }
            return true;
        }
    }
}
