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
        protected readonly Random _random = new Random();
        protected int AddHappines(int happines)
        {
            happines += _random.Next(1, 10);
            return Math.Min(100, happines);
        }

        protected int AddHungry(int hungry)
        {
            hungry += _random.Next(1, 10);
            return Math.Min(100, hungry);
        }

        protected int AddSleep(int sleep)
        {
            sleep += _random.Next(1, 10);
            return Math.Min(100, sleep);
        }

        protected int OddHappiness(int happiness)
        {
            happiness -= _random.Next(1, 10);
            return Math.Max(0, happiness);
        }

        protected int OddHungry(int hungry)
        {
            hungry -= _random.Next(1, 10);
            return Math.Max(0, hungry);
        }

        protected int OddSleep(int sleep)
        {
            sleep -= _random.Next(1, 10);
            return Math.Max(0, sleep);
        }

        protected int NaturalOddHappiness(int happiness)
        {
            happiness -= _random.Next(1, 4);
            return Math.Max(0, happiness);
        }

        protected int NaturalOddHungry(int hungry)
        {
            hungry -= _random.Next(1, 4);
            return Math.Max(0, hungry);
        }

        protected int NaturalOddSleep(int sleep)
        {
            sleep -= _random.Next(1, 4);
            return Math.Max(0, sleep);
        }


        protected int MoneyFromWork()
        {
            return _random.Next(1, 10);
        }
        
        protected int LiveChanged(
            int happiness, 
            int hungry, 
            int sleep,
            int currentLive
            )
        {
            if (happiness <= 0 || hungry <= 0 || sleep <= 0)
            {
                int lifeLoss = _random.Next(1, 5);

                if (happiness <= 0 && hungry <= 0)
                {
                    lifeLoss *= 2;
                }

                if (happiness <= 0 && hungry <= 0 && sleep <= 0)
                {
                    lifeLoss *= 3;
                }

                return Math.Max(0, currentLive - lifeLoss);
            }
            else if (happiness >= 100 && hungry >= 100 && sleep >= 100)
            {
                int lifeGain = _random.Next(1, 5);
                return Math.Min(100, currentLive + lifeGain);
            }

            return currentLive;
        }

        protected int ProcessPurchase(int money, int cost)
        {
            return money - cost;
        }

        protected bool CanAfford(int money, int cost)
        {
            return money >= cost;
        }

        protected bool IsHappinessCritical(int happiness)
        {
            return happiness < 30;
        }

        protected bool IsHungtyCritical(int hungry)
        {
            return hungry < 30;
        }

        protected bool IsSleepCritical(int sleep)
        {
            return sleep < 30;
        }

        protected bool IsLifeCritical(int life)
        {
            return life < 30;
        }

        protected bool IsDead(int life)
        {
            return life <= 0;
        }

    }
}
