using bieda_simsy.GameMechanics.Interfaces;

namespace bieda_simsy.GameMechanics.Abstract
{
    internal abstract class StatModifier : IStatsModifier
    {
        private readonly Random _random = new Random();
        
        public int AddStats(int stats, int value)
        {
            stats += value;

            if (stats > 100)
            {
                stats = 100;
            }

            return stats;
        }

        public int OddStats(int stats, int value)
        {
            stats -= _random.Next(1, value + 1);
            return Math.Max(0, stats);
        }

        public int AddOddMoney()
        {
            return _random.Next(1, 11);
        }
        
        public int LiveChanged(
            int happiness, 
            int hungry, 
            int sleep,
            int currentLive
            )
        {
            if (happiness <= 0 || hungry <= 0 || sleep <= 0)
            {
                int lifeLoss = _random.Next(1, 6);

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
                int lifeGain = _random.Next(1, 6);
                return Math.Min(100, currentLive + lifeGain);
            }

            return currentLive;
        }

        public int PayForSomething(int money, int cost)
        {
            return Math.Max(0, money - cost);
        }

        public bool CanAfford(int money, int cost)
        {
            return money >= cost;
        }

        public bool IsDead(int life)
        {
            return life <= 0;
        }
    }
}
