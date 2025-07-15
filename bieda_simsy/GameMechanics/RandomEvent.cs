using bieda_simsy.GameMechanics.Enums;
using bieda_simsy.GameMechanics.Models;

namespace bieda_simsy.GameMechanics
{
    internal class RandomEvent 
    {
        private static Random _random = new Random();
        private StatModifier _modifier;

        public RandomEvent()
        {
            _modifier = new StatModifier();
        }

        /// <summary>
        /// creates and processes a random event (good or bad) that modifies the player's statistics.
        /// </summary>
        public void GenerateEvent(Player player)
        {
            EventEnum eventType = (EventEnum)_random.Next(1,4);

            switch (eventType)
            {
                case EventEnum.GoodEvent:
                    GoodEvent(player);
                    break;
                case EventEnum.BadEvent:
                    BadEvent(player);
                    break;
                case EventEnum.NoEvent:
                    break;
            }
        }

        /// <summary>
        /// increases random ststistics
        /// </summary>
        private void GoodEvent(Player player)
        {
            int change = _random.Next(1, 11);
            int random = _random.Next(1, 7);

            
            switch (random)
            {
                case 1:
                    int oldHappiness = player.Happiness;
                    player.Happiness = _modifier.AddStats(player.Happiness, change);
                    int happinessGained = player.Happiness - oldHappiness;
                    Console.WriteLine($"\n{player.Name} is happy today.\n+{happinessGained} happiness\n");
                    break;

                case 2:
                    int oldHungry = player.Hungry;
                    player.Hungry = _modifier.AddStats(player.Hungry, change);
                    int hungryGained = player.Hungry - oldHungry;
                    Console.WriteLine($"\n{player.Name} enjoyed the food.\n+{hungryGained} hungry\n");
                    break;

                case 3:
                    int bonusMoney = _modifier.AddOddMoney();
                    player.Money += bonusMoney;
                    Console.WriteLine($"\n{player.Name} boss is nice, send extra money.\n+{bonusMoney} coins\n");
                    break;

                case 4:
                    int oldSleep = player.Sleep;
                    player.Sleep = _modifier.AddStats(player.Sleep, change);
                    int sleepGained = player.Sleep - oldSleep;
                    Console.WriteLine($"\n{player.Name} have a good dream.\n+{sleepGained} sleep\n");
                    break;

                case 5:
                    int oldPurity = player.Purity;
                    player.Purity = _modifier.AddStats(player.Purity, change);
                    int purityGained = player.Purity - oldPurity;
                    Console.WriteLine($"\n{player.Name} think is a cat.\n+{purityGained} purity\n");
                    break;

                case 6:
                    int refund = _random.Next(5, 12);
                    player.Money += refund;
                    int oldHappiness2 = player.Happiness;
                    player.Happiness = _modifier.AddStats(player.Happiness, change / 2);
                    int happinessGained2 = player.Happiness - oldHappiness2;
                    Console.WriteLine($"{player.Name} found money on the floor.\n+{refund} coins\n+{happinessGained2} happiness");
                    break;
            }
        }

        /// <summary>
        /// reduces the random ststistics
        /// </summary>
        private void BadEvent(Player player)
        {
            int change = _random.Next(1, 11);
            int change2 = _random.Next(1, 11);
            int random = _random.Next(1, 7);
            
            switch (random)
            {
                case 1:
                    int oldHappiness = player.Happiness;
                    int oldLive = player.Live;
                    player.Happiness = _modifier.OddStats(player.Happiness, change);
                    player.Live = _modifier.OddStats(player.Live, change2);
                    int happinessLost = oldHappiness - player.Happiness;
                    int liveLost = oldLive - player.Live;
                    Console.WriteLine($"\n{player.Name} struck out.\n-{happinessLost} happiness\n-{liveLost} live");
                    break;

                case 2:
                    int oldHungry = player.Hungry;
                    player.Hungry = _modifier.OddStats(player.Hungry, change);
                    int hungryLost = oldHungry - player.Hungry;
                    Console.WriteLine($"\n{player.Name} found a food on floor.\n-{hungryLost} hungry\n");
                    break;

                case 3:
                    int bill = _modifier.AddOddMoney();
                    player.Money = Math.Max(0, player.Money - bill);
                    Console.WriteLine($"\n{player.Name} have extra bill.\n-{bill} coins\n");
                    break;

                case 4:
                    int oldSleep = player.Sleep;
                    player.Sleep = _modifier.OddStats(player.Sleep, change);
                    int sleepLost = oldSleep - player.Sleep;
                    Console.WriteLine($"\n{player.Name} have a nightmare.\n-{sleepLost} sleep\n");
                    break;

                case 5:
                    int oldPurity = player.Purity;
                    player.Purity = _modifier.OddStats(player.Purity, change);
                    int purityLost = oldPurity - player.Purity;
                    Console.WriteLine($"\n{player.Name} make a Nurgle poop.\n-{purityLost} purity\n");
                    break;

                case 6:
                    int stolen = _random.Next(5, 12);
                    player.Money = Math.Max(0, player.Money - stolen);
                    int oldHappiness2 = player.Happiness;
                    player.Happiness = _modifier.OddStats(player.Happiness, change / 2);
                    int happinessLost2 = oldHappiness2 - player.Happiness;
                    Console.WriteLine($"{player.Name} was robbed by robber poop.\n-{stolen} coins\n-{happinessLost2} happiness");
                    break;
            }
        }
    }
}