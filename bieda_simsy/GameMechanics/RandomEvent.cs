using bieda_simsy.GameMechanics.Abstract;
using bieda_simsy.GameMechanics.Interfaces;
using System;
using System.Collections.Generic;

namespace bieda_simsy.GameMechanics
{
    internal class RandomEvent : StatModifier
    {
        private static Random _random = new Random();

        public Dictionary<string, int> GenerateEvent(
            int live,
            int money,
            int happiness,
            int hungry,
            int sleep,
            int purity,
            string name
            )
        {
            Dictionary<string, int> results = new Dictionary<string, int>
            {
                {"live", live},
                {"money", money},
                {"happiness", happiness},
                {"hungry", hungry},
                {"sleep", sleep},
                {"purity", purity}
            };

            if (_random.Next(1, 101) <= 25)
            {
                bool isGoodEvent = _random.Next(1, 101) <= 60;

                if (isGoodEvent)
                {
                    GoodEvent(results, name);
                }
                else
                {
                    BadEvent(results, name);
                }
            }

            return results;
        }

        private void GoodEvent(Dictionary<string, int> results, string name)
        {
            int change = _random.Next(1, 11);
            int random = _random.Next(1, 7);

            
            switch (random)
            {
                case 1: 
                    results["happiness"] = AddStats(results["happiness"], change);
                    Console.WriteLine($"\n{name} is happy today.\n+{change} happiness\n");
                    break;
                case 2: 
                    results["hungry"] = AddStats(results["hungry"], change);
                    Console.WriteLine($"\n{name} enjoyed the food.\n+{change} hungry\n");
                    break;
                case 3:
                    int bonusMoney = AddOddMoney();
                    results["money"] += bonusMoney;
                    Console.WriteLine($"\n{name} boss is nice, send extra money.\n+{bonusMoney} coins\n");
                    break;
                case 4:
                    results["sleep"] = AddStats(results["sleep"], change);
                    Console.WriteLine($"\n{name} have a good dream.\n+{change} sleep\n");
                    break;

                case 5:
                    results["purity"] = AddStats(results["purity"], change);
                    Console.WriteLine($"\n{name} think is a cat.\n+{change} purity\n");
                    break;

                case 6:
                    int refund = _random.Next(5, 12);
                    results["money"] += refund;
                    results["happiness"] = AddStats(results["happiness"], change / 2);
                    Console.WriteLine($"{name} found monety in the floor.\n+{refund} coins\n+{change} happiness");
                    break;
            }
        }

        private void BadEvent(Dictionary<string, int> results, string name)
        {
            int change = _random.Next(1, 11);
            int change2 = _random.Next(1, 11);
            int random = _random.Next(1, 7);
            
            switch (random)
            {
                case 1:
                    results["happiness"] = OddStats(results["happiness"], change);
                    results["live"] = OddStats(results["live"], change2);
                    Console.WriteLine($"\n{name} struck out .\n+{change} happiness\n-{change2} live");
                    break;
                case 2:
                    results["hungry"] = OddStats(results["hungry"], change);
                    Console.WriteLine($"\n{name} found a food in floor.\n-{change} hungry\n");
                    break;
                case 3:
                    int bonusMoney = AddOddMoney();
                    results["money"] -= bonusMoney;
                    Console.WriteLine($"\n{name} have extra bill .\n-{bonusMoney} coins\n");
                    break;
                case 4:
                    results["sleep"] = OddStats(results["sleep"], change);
                    Console.WriteLine($"\n{name} have a nightmare.\n-{change} sleep\n");
                    break;

                case 5:
                    results["purity"] = OddStats(results["purity"], change);
                    Console.WriteLine($"\n{name} make a Nurgle poop.\n-{change} purity\n");
                    break;
                case 6:
                    int refund = _random.Next(5, 12);
                    results["money"] -= refund;
                    results["happiness"] = AddStats(results["happiness"], change / 2);
                    Console.WriteLine($"{name} was robbed by robber poop.\n-{refund} coins\n-{change} happiness");
                    break;
            }
        }
    }
}