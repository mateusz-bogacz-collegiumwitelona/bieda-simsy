using bieda_simsy.GameMechanics.Interfaces;

namespace bieda_simsy.GameMechanics.Abstract
{
    internal class StatModifier : IStatsModifier
    {
        private readonly Random _random = new Random();
        private const int MAX_STAT = 100; // max value of stats
        private const int MIN_STAT = 0; // min value of stats

        /// <summary>
        /// add stats points making sure they don't exceed 100
        /// </summary>
        public int AddStats(int stats, int value) => Math.Min(MAX_STAT, stats + _random.Next(1, value + 1));

        /// <summary>
        /// subtracts statist's points making sure they do not exceed 0
        /// </summary>
        public int OddStats(int stats, int value) => Math.Max(MIN_STAT, stats - _random.Next(1, value + 1));

        /// <summary>
        /// generate random number of coin
        /// this coin can be add or odd 
        /// </summary>
        public int AddOddMoney() => _random.Next(1, 11);

        /// <summary>
        /// changes life points according to the number of stats at minus or plus
        /// </summary>
        /// <returns></returns>
        public int LiveChanged(
            int happiness, 
            int hungry, 
            int sleep,
            int currentLive,
            int purity
            )
        {
            if (happiness <= 0 || hungry <= 0 || sleep <= 0 || purity <= 0)
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

                if (happiness <= 0 && hungry <= 0 && sleep <= 0 && purity <= 0)
                {
                    lifeLoss *= 5;
                }

                return Math.Max(0, currentLive - lifeLoss);
            }
            else if (happiness == 100 && hungry == 100 && sleep == 100 && purity == 100)
            {
                int lifeGain = _random.Next(1, 6);
                return Math.Min(100, currentLive + lifeGain);
            }

            return currentLive;
        }

        /// <summary>
        /// subtracts money for purchased items,
        /// makes sure that it is impossible to have less than 0 coins
        /// </summary>
        public int PayForSomething(int money, int cost) => Math.Max(0, money - cost);

        /// <summary>
        /// checks if the player can afford the item
        /// </summary>
        public bool CanAfford(int money, int cost) => money >= cost;

        /// <summary>
        /// chcecks if player is still alive
        /// </summary>
        public bool IsDead(int life) => life <= 0;

        /// <summary>
        /// limits the value of the statistic to the range of 0-100.
        /// </summary>
        public int ClampStat(int value) => Math.Clamp(value, MIN_STAT, MAX_STAT);
    }
}
