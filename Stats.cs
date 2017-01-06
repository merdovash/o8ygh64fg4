using Game.Loggers;
using Game.Units.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Units.Heroes
{
    class Stats
    {
        public Stats(int[] p, int AS, int[] s, double[] i, Hero h)
        {
            damage = p[0];
            def = p[1];
            basicHealth = p[2];

            baseAttackSpeed = AS;

            baseSTR = s[0];
            baseDEX = s[1];
            baseINT = s[2];

            strI = i[0];
            dexI = i[1];
            intI = i[2];

            clas = s[3];

            Prepare();

            buffs = new Buffs(h);
        }

        public void Prepare()
        {
            currentHealth = MaxHealth;
            sp = 0;
            dead = false;
            CalculateStats();
        }

        private void CalculateStats()
        {
            curSTR = baseSTR + (int)(strI * level);
            curDEX = baseDEX + (int)(dexI * level);
            curINT = baseINT + (int)(intI * level);

            currentAttackSpeed = baseAttackSpeed;
        }

        //stats part
        private int clas;
        private int baseSTR;
        private int baseDEX;
        private int baseINT;

        private double strI;
        private double dexI;
        private double intI;

        private int curSTR;
        private int curDEX;
        private int curINT;

        public int this[int i]
        {
            get
            {
                switch (i)
                {
                    case 1: return curSTR;
                    case 2: return curDEX;
                    case 3: return curINT;
                    default: return 0;
                }
            }
        }

        public int Class
        {
            get
            {
                return clas;
            }
        }

        //attack speed part
        private int baseAttackSpeed;
        private int currentAttackSpeed;
        private long lastAttack = 0;
        public int AttackSpeed
        {
            get
            {
                return currentAttackSpeed;
            }
        }

        public bool AttackReady(long currentTime)
        {
            if (currentTime - currentAttackSpeed > lastAttack)
            {
                return true;
            }
            return false;
        }

        public void Attack(long currentTime)
        {
            lastAttack = currentTime;
        }


        //damage part
        private int damage;
        public int Damage
        {
            get
            {
                switch (clas)
                {
                    case 1:
                        return damage + curSTR;
                    case 2:
                        return damage + curDEX * 2;
                    case 3:
                        return (int)(damage + curINT * 0.5);
                    default:
                        return damage;
                }
            }
        }

        //def part
        private int def;
        public int Def
        {
            get
            {
                return (int)(def + 0.1 * curDEX);
            }
        }

        //magicdef part
        private int magicDef;

        public int MagicDef
        {
            get
            {
                return (int)(magicDef + 0.1 * curINT);
            }
        }

        //health part
        private long basicHealth;
        private long currentHealth;
        public long MaxHealth
        {
            get
            {
                return basicHealth + curSTR * 15 + 5 * level;
            }
        }

        public long CurrentHealth
        {
            get
            {
                return currentHealth;
            }
        }

        public double CurrentHealthPercent
        {
            get
            {
                return currentHealth / MaxHealth;
            }
        }

        public void HealthAffect(long a)
        {
            currentHealth -= a;
            if (currentHealth > MaxHealth)
            {
                currentHealth = MaxHealth;
            }
            else if (currentHealth <= 0)
            {
                currentHealth = 0;
                dead = true;
            }
        }

        //exp part
        private int level = 0;
        private long exp;
        private long nextLevelExp;
        public int Level
        {
            get
            {
                return level;
            }
        }

        public long CurrentExp
        {
            get
            {
                return exp;
            }
        }

        public void AddExp(long e)
        {
            exp += e;
            if (exp >= nextLevelExp)
            {
                level += 1;
                exp -= nextLevelExp;
                nextLevelExp += 50 * level;
            }
        }

        public void SetLevel(int level, long exp)
        {
            this.level = level;
            this.exp = exp;
            CalculateNextLevelExp();
        }

        private void CalculateNextLevelExp()
        {
            nextLevelExp = 50 + 25 * (level - 1)*level/2;
        }

        //dead part
        private bool dead;
        public bool isDead
        {
            get
            {
                return dead;
            }
        }

        //sp part
        private double sp;
        public double SP
        {
            get
            {
                return sp;
            }
        }

        public void AddSP(double d)
        {
            sp += d + (clas == 3 ? (((double)curINT) / 500) : 0);
        }

        public bool SpellReady
        {
            get
            {
                if (sp >= 1)
                {
                    sp = 0;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //effects part
        Buffs buffs;
        public void AddEffect(Effect effect)
        {
            buffs.Add(effect);            
        }

        public void Update(long currentTime)
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                buffs[i].Update(currentTime);
            }
        }
    }
}
