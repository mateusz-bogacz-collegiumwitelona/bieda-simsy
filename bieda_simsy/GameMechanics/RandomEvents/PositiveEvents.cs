using bieda_simsy.GameMechanics.Abstract;
using bieda_simsy.GameMechanics.Interfaces;

namespace bieda_simsy.GameMechanics.RandomEvents
{
    internal class PositiveEvents : StatModifier, IEvents
    {
        private static Random _random = new Random();
        private int change = 0;
        private int oldStat = 0;

        public Dictionary<string, int> GenerateEvent(
            int live,
            int money,
            int happiness,
            int hungry,
            int sleep,
            int purity,
            string name)
        {
            Dictionary<string, int> results = new Dictionary<string, int>();

            int eventType = _random.Next(1, 7);

            switch (eventType)
            {
                case 1:
                    results["money"] = NiceBoss(money, name);
                    break;
                case 2:
                    results["sleep"] = Nap(sleep, name);
                    break;
                case 3:
                    results["happiness"] = GoodDay(happiness, name);
                    break;
                case 4:
                    results["hungry"] = FoodInFloor(hungry, name);
                    break;
                case 5:
                    results["purity"] = CatLike(purity, name);
                    break;
                case 6:
                    results["happiness"] = DontWorryBeHappy(happiness, name);
                    break;
            }

            return results;
        }

        private int NiceBoss(int money, string name)
        {
            change = AddOddMoney();
            money += change;
            Console.WriteLine($"\n{name} is nice, send extra money.\n+{change} coins\n");
            return money;
        }

        private int Nap(int sleep, string name)
        {
            oldStat = sleep;
            sleep = AddStats(sleep, 10);
            change = sleep - oldStat;
            Console.WriteLine($"\n{name} took a nap. They sleep a lot\n+{change} sleep\n");
            return sleep;
        }

        private int GoodDay(int happiness, string name)
        {
            oldStat = happiness;
            happiness = AddStats(happiness, 5);
            change = happiness - oldStat;
            Console.WriteLine($"\nIt was a good day.\n+{change} happiness\n");
            return happiness;
        }

        private int FoodInFloor(int hungry, string name)
        {
            oldStat = hungry;
            hungry = AddStats(hungry, 10);
            change = hungry - oldStat;
            Console.WriteLine($"\n{name} found a food in floor.\n+{change} hungry\n");
            return hungry;
        }

        private int CatLike(int purity, string name)
        {
            oldStat = purity;
            purity = AddStats(purity, 10);
            change = purity - oldStat;
            Console.WriteLine($"\n{name} think is a cat.\n+{change} purity\n");
            return purity;
        }

        private int DontWorryBeHappy(int happiness, string name)
        {
            oldStat = happiness;
            happiness = AddStats(happiness, 10);
            change = happiness - oldStat;
            Console.WriteLine($"\n{name} is happy today.\n+{change} happiness\n");
            return happiness;
        }
    }
}
