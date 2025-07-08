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
        
        protected int AddStats(int stats, int value)
        {
            stats += value;
            int add = Math.Min(100, stats);

            stats =+ add;

            if (stats > 100)
            {
                stats = 100;
            }

            return stats;

        }

        protected int OddStats(int stats, int value)
        {
            stats += value;
            return Math.Max(0, stats);
        }

        protected int NaturalOddStats(int stats, int value)
        {
            stats -= _random.Next(1, value);
            return Math.Max(0, stats);
        }

        protected int AddOddMoney()
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
            else if (happiness == 100 && hungry == 100 && sleep == 100)
            {
                int lifeGain = _random.Next(1, 5);
                return Math.Min(100, currentLive + lifeGain);
            }

            return currentLive;
        }

        protected int PayForSomething(int money, int cost)
        {
            return money - cost;
        }

        protected bool CanAfford(int money, int cost)
        {
            return money >= cost;
        }

        protected bool IsDead(int life)
        {
            return life <= 0;
        }
    }
}
