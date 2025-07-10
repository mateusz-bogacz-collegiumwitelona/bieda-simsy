using bieda_simsy.GameMechanics.Abstract;
using bieda_simsy.GameMechanics.Interfaces;

namespace bieda_simsy.GameMechanics.RandomEvents
{
    internal class NegativeEvents : StatModifier, IEvents
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
            string name
            )
        {
            Dictionary<string, int> results = new Dictionary<string, int>();

            int eventType = _random.Next(1, 7);
            switch (eventType)
            {
                case 1:
                    results["happiness"] = StupidBoss(happiness, name);
                    break;
                case 2:
                    results["live"] = NurglePoop(live, name);
                    break;
                case 3:
                    results["purity"] = PoopRightNow(purity, name);
                    break;
                case 4:
                    results["money"] = RooberPoop(money, name);
                    break;
                case 5:
                    results["sleep"] = BadCoffe(sleep, name);
                    break;
                case 6:
                    results["hungry"] = FoodInFloor(hungry, name);
                    break;
            }

            return results;
        }

        private int StupidBoss(int happiness, string name)
        {
            oldStat = happiness;
            happiness = OddStats(happiness, 10);
            change = oldStat - happiness;
            Console.WriteLine($"{name} boss is dumb.\n-{change} happiness");
            return happiness;
        }

        private int NurglePoop(int live, string name)
        {
            oldStat = live;
            live = OddStats(live, 10);
            change = oldStat - live;
            Console.WriteLine($"{name} is sad. They poop a Nurgle poop\n-{change} live");
            return live;
        }

        private int PoopRightNow(int purity, string name)
        {
            oldStat = purity;
            purity = OddStats(purity, 10);
            change = oldStat - purity;
            Console.WriteLine($"{name} pooped after eating.\n-{change} purity");
            return purity;
        }

        private int RooberPoop(int money, string name)
        {
            change = AddOddMoney();
            money -= change;
            Console.WriteLine($"{name} was robbed by robbed poop, its very stressfull.\n-{change} coins");
            return money;
        }

        private int BadCoffe(int sleep, string name)
        {
            oldStat = sleep;
            sleep = OddStats(sleep, 10);
            change = oldStat - sleep;
            Console.WriteLine($"{name} drink a bad coffet, they don't work.\n-{change} sleep");
            return sleep;
        }

        private int FoodInFloor(int hungry, string name)
        {
            oldStat = hungry;
            hungry = OddStats(hungry, 10);
            change = oldStat - hungry;
            Console.WriteLine($"\n{name} found a food in floor.\n-{change} hungry\n");
            return hungry;
        }
    }
}
