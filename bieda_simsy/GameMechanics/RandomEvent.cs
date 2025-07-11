﻿using bieda_simsy.GameMechanics.Abstract;
using bieda_simsy.GameMechanics.Enums;
using bieda_simsy.GameMechanics.Interfaces;
using System;
using System.Collections.Generic;

namespace bieda_simsy.GameMechanics
{
    internal class RandomEvent : StatModifier
    {
        private static Random _random = new Random();

        /// <summary>
        /// creates and processes a random event (good or bad) that modifies the player's statistics.
        /// </summary>
        public Dictionary<string, int> GenerateEvent(
            EventEnum eventType,
            int live,
            int money,
            int happiness,
            int hungry,
            int sleep,
            int purity,
            string name
            )
        {
            var results = new Dictionary<string, int>
            {
                {"live", live},
                {"money", money},
                {"happiness", happiness},
                {"hungry", hungry},
                {"sleep", sleep},
                {"purity", purity}
            };

            switch (eventType)
            {
                case EventEnum.GoodEvent:
                    GoodEvent(results, name);
                    break;

                case EventEnum.BadEvent:
                    BadEvent(results, name);
                    break;
            }

            return results;
        }

        /// <summary>
        /// increases random ststistics
        /// </summary>
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

        /// <summary>
        /// reduces the random ststistics
        /// </summary>

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