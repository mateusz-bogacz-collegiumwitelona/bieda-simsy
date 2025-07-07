using System;
using System.Collections.Generic;
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
                return live + random.Next(1, 10);
            }
            else if (hungry > 0 && happines > 0)
            {
                Random random = new Random();
                return live + (random.Next(1, 10) * 2);
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

        protected int OddLive(int live, int happiness, int hungry)
        {
            if (happiness < 0 || hungry < 0)
            {
                Random random = new Random();
                return live - random.Next(1, 10);
            }
            else if (happiness < 0 && hungry < 0)
            {
                Random random = new Random();
                return live - (random.Next(1, 10) * 2);
            }
            return live;
        }
    }
}
