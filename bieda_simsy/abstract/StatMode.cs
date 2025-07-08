using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bieda_simsy.@abstract
{
    internal abstract class StatMode
    {

        protected int AddHappines(int happines)
        {
            Random random = new Random();

            happines += random.Next(1, 10);

            if (happines > 100)
            {
                return happines = 100;
            }

            return happines;
        }

        protected int AddHungry(int hungry)
        {
            Random random = new Random();

            hungry += random.Next(1, 10);

            if (hungry > 100)
            {
                return hungry = 100;
                AddLive(0, hungry, 0);
            }

            return hungry;
        }

        protected int AddLive(int happines, int hungry, int live)
        {
            Random rand = new Random();

            if (happines > 0 && hungry > 0)
            {
                live += (rand.Next(1, 10) * 2);
            }
            else if (happines > 0 || hungry > 0)
            {
                live += rand.Next(1, 10);
            }
            
            if (live > 100)
            {
                return live = 100;
            }

            return live;
        }

        protected int OddHappiness(int happiness)
        {
            Random random = new Random();
            happiness -= random.Next(1, 10);

            if (happiness < 0)
            {
                return happiness = 0;
            }

            return happiness;
        }

        protected int OddHungry(int hungry)
        {
            Random random = new Random();
            hungry -= random.Next(1, 10);

            if (hungry < 0)
            {
                OddLive(0, hungry, 0);
                return hungry = 0;
            }

            return hungry;
        }

        protected int OddLive(int happiness, int hungry, int live)
        {

            Random random = new Random();

            if (happiness < 0 && hungry < 0)
            {
                live -= random.Next(1, 10);
            }
            else if (happiness < 0 || hungry < 0)
            {
                live -= random.Next(1, 5);
            }

            if (live < 0)
            {
                live = 0;
            }

            return live;
        }

        protected int OddMoney(int money)
        {
            return money - 10;
        }

        protected int Work(int money)
        {
            Random random = new Random();
            return money + random.Next(1, 10);
        }

        protected int BuyFood(int hungry)
        {
            return hungry + 10;
        }

        protected int BuyHappiness(int happiness)
        {
            return happiness + 10;
        }

        protected int AddSleep(int sleep)
        {
            Random random = new Random();
            sleep += random.Next(1, 10);
            if (sleep > 100)
            {
                return sleep = 100;
            }
            return sleep;
        }

        protected int OddSleep(int sleep)
        {
            Random random = new Random();
            sleep -= random.Next(1, 10);
            if (sleep < 0)
            {
                return sleep = 0;
            }
            return sleep;
        }

    }
}
