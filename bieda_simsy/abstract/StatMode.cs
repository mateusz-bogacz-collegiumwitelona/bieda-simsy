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

        protected bool _isAlive;

        protected int AddHappines(int happines)
        {
            Random random = new Random();
            return happines + random.Next(1, 10);
        }

        protected int AddHungry(int hungry)
        {
            Random random = new Random();
            return hungry + random.Next(1, 10);
        }

        protected int AddLive(int happines, int hungry, int live)
        {

            if (hungry > 0 || happines > 0)
            {
                Random random = new Random();

                live += random.Next(1, 10);
                
                if (live >= 100)
                {
                    return live = 100;
                }

                return live;
            }
            else if (hungry > 0 && happines > 0)
            {
                Random random = new Random();
                live += (random.Next(1, 10) * 2);
             
                if (live >= 100)
                {
                    return live = 100;
                }

                return live;
            }

            return live;
        }


        protected int OddHappiness(int happiness)
        {
            Random random = new Random();
            return happiness - random.Next(1, 10);
        }

        protected int OddHungry(int hungry)
        {
            Random random = new Random();
            return hungry - random.Next(1, 10);
        }

        protected int OddLive(int live, int happiness, int hungry, bool isAlive)
        {
            if (happiness < 0 || hungry < 0)
            {
                Random random = new Random();
                live -= random.Next(1, 10);

                if (live <= 0)
                {
                    _isAlive = false;
                }
                else
                {
                    _isAlive = true;
                }

                return live;
            }
            else if (happiness < 0 && hungry < 0)
            {
                Random random = new Random();
                live = -(random.Next(1, 10) * 2);

                if (live <= 0)
                {
                    _isAlive = false;
                }
                else
                {
                    _isAlive = true;
                }

                return live;
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

        protected bool isDead()
        {
            return _isAlive;
        }
    }
}
