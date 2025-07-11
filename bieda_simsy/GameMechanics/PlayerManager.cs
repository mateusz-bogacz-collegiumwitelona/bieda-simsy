using bieda_simsy.GameMechanics.Abstract;
using bieda_simsy.GameMechanics.Interfaces;
using bieda_simsy.GameMechanics.RandomEvents;
using bieda_simsy.Saved.Interfaces;

namespace bieda_simsy.GameMechanics
{
    internal class PlayerManager : StatModifier, ISaved, IStats, IStatsModifier
    {
        private string _name;
        private int _live;
        private int _money;
        private int _happiness;
        private int _hungry;
        private int _sleep;
        private bool _isAlive;
        private int _actionToBill;
        private readonly object _lock = new object();
        private int _purity;

        public bool IsAlive => _isAlive;
        public string FileName =>
            string.IsNullOrEmpty(_name)
            ? "default_save" : _name.ToLower().Replace(" ", "_");

        public PlayerManager()
        {
            _name = string.Empty;
            _live = 100;
            _money = 10;
            _happiness = 100;
            _hungry = 100;
            _isAlive = true;
            _sleep = 100;
            _actionToBill = 0;
            _purity = 100;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int Live => _live;

        public int Money => _money;
        public int Hungry => _hungry;
        public int Sleep => _sleep;
        public int Happiness => _happiness;
        public int Purity => _purity;
        public bool Alive => _isAlive;

        protected string SetName()
        {
            Console.Write("Enter your name: ");
            _name = Console.ReadLine();
            return _name;
        }

        protected void SetName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _name = name;
            }
            else
            {
                _name = "Unnamed";
            }
        }


        protected void DecayStats(
            bool isAlive, 
            int happiness, 
            int hungry,
            int sleep,
            int purity)
        {
            if (!_isAlive) return;

            int happinessValue = 10;
            int hungryValue = 10;
            int sleepValue = 10;
            int purityValue = 10;


            _happiness = OddStats(_happiness, happinessValue);
            _hungry = OddStats(_hungry, hungryValue);
            _sleep = OddStats(_sleep, sleepValue);
            _purity = OddStats(_purity, purityValue);

            _live = LiveChanged(_happiness, _hungry, _sleep, _live);

            if (IsDead(_live))
            {
                _isAlive = false;
                CheckIfAlive();
            }
        }

        private void CheckIfAlive()
        {
            if (_isAlive)
            {
                Console.WriteLine($"\n{_name} has died. Game over.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        protected void PlayWith(int value)
        {
            Console.Clear();
            int oldHappiness = _happiness;
            _happiness = AddStats(_happiness, value);
            int happinessGained = _happiness - oldHappiness;
            Console.WriteLine($"You played with {_name}. Happiness increased by {happinessGained}");

            PostAction();
        }

        protected void Feed(int value)
        {
            int oldHungry = _hungry;
            _hungry = AddStats(_hungry, value);
            int hungryGained = _hungry - oldHungry;
            Console.WriteLine($"You fed {_name}. Hunger increased by {hungryGained}");
            PostAction();
        }

        protected void YouMustWork(int value)
        {
            Console.Clear();

            int moneyFromWork = AddOddMoney();
            int oldHappiness = _happiness;
            int oldHungry = _hungry;
            int oldSleep = _sleep;
            int oldPurity = _purity;
            _money += moneyFromWork;



            _happiness = OddStats(_happiness, value);
            _hungry = OddStats(_hungry, value);
            _sleep = OddStats(_sleep, value);
            _purity = OddStats(_purity, value);

            Console.WriteLine($"You worked and earned {moneyFromWork} money. Current money: {_money}");
            Console.WriteLine($"But work is boring and you have:");
            Console.WriteLine($" {_name} has lost {oldHappiness - _happiness} happiness,\n " +
                              $"{oldHungry - _hungry} hunger,\n " +
                              $"{oldSleep - _sleep} sleep,\n " +
                              $"{oldPurity - _purity} purity.");

            PostAction();
        }

        protected void BuySomething(string itemname, int choice, int price, int value)
        {

            Console.Clear();
            if (CanAfford(_money, price))
            {
                _money = PayForSomething(_money, price);
                switch (choice)
                {
                    case 1:
                        _hungry = AddStats(_hungry, value);
                        Console.WriteLine($"You bought {itemname}. {_name} current have {_hungry} of hungry");
                        break;
                    case 2:
                        _happiness = AddStats(_happiness, value);
                        Console.WriteLine($"You bought {itemname}. {_name} current have {_happiness} of happiness");
                        break;
                    case 3:
                        _sleep = AddStats(_sleep, value);
                        Console.WriteLine($"You bought {itemname}. {_name} current have {_sleep} of sleepness");
                        break;
                    case 4:
                        _purity = AddStats(_purity, value);
                        Console.WriteLine($"You bought {itemname}. {_name} current have {_purity} of purity");
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Not enough money to buy this item.");
            }
            PostAction();
        }

        protected void GoToSleep(int value)
        {
            Console.Clear();

            int oldSleep = _sleep;
            _sleep = AddStats(_sleep, value);
            int sleepGained = _sleep - oldSleep;
            Console.WriteLine($"You slept. Sleep increased by {sleepGained}");

            PostAction();
        }

        protected void WashYourself(int value)
        {
            Console.Clear();

            int oldPurity = _purity;
            _purity = AddStats(_purity, value);
            int purityGained = _purity - oldPurity;
            Console.WriteLine($"You washed yourself. Purity increased by {purityGained}");

            PostAction();
        }

        protected void MustPayTax()
        {
            if (_actionToBill >= 5)
            {
                int tax = AddOddMoney();
                Console.WriteLine($"You must pay tax - {tax} coins");
                _money = PayForSomething(_money, tax);
                _actionToBill = 0;
            }
        }

        public void LoadData(Dictionary<string, object> data)
        {
            _name = data["name"]?.ToString() ?? "Unnamed";
            _live = Convert.ToInt32(data["live"]);
            _money = Convert.ToInt32(data["money"]);
            _happiness = Convert.ToInt32(data["happiness"]);
            _hungry = Convert.ToInt32(data["hungry"]);
            _sleep = Convert.ToInt32(data["sleep"]);
            _isAlive = Convert.ToBoolean(data["isAlive"]);
        }

        public Dictionary<string, object> GetData()
        {
            return new Dictionary<string, object>
                {
                    { "name", _name },
                    { "live", _live },
                    { "money", _money },
                    { "happiness", _happiness },
                    { "hungry", _hungry },
                    { "sleep", _sleep },
                    { "purity", _purity },
                    { "isAlive", _isAlive }
                };
        }

        protected void GenerateRandomEvent()
        {
            Random random = new Random();
            int rand = random.Next(1,4);


            RandomEvent randomEvent = new RandomEvent();
            
            Dictionary<string, int> eventResults;

            switch (rand)
            {
                case 1:
                    eventResults = randomEvent.GenerateEvent(_live, _money, _happiness, _hungry, _sleep, _purity, _name);
                    break;
                case 2:
                    eventResults = randomEvent.GenerateEvent(_live, _money, _happiness, _hungry, _sleep, _purity, _name);
                    ApplyEventsResoult(eventResults);
                    break;
                default:
                    return;
            }
        }

        private void ApplyEventsResoult(Dictionary<string, int> eventResults)
        {
            if (eventResults.ContainsKey("live"))
                _live = ClampStat(eventResults["live"]);
            if (eventResults.ContainsKey("happiness"))
                _happiness = ClampStat(eventResults["happiness"]);
            if (eventResults.ContainsKey("hungry"))
                _hungry = ClampStat(eventResults["hungry"]);
            if (eventResults.ContainsKey("sleep"))
                _sleep = ClampStat(eventResults["sleep"]);
            if (eventResults.ContainsKey("purity"))
                _purity = ClampStat(eventResults["purity"]);
            if (eventResults.ContainsKey("money"))
                _money = Math.Max(0, eventResults["money"]);

        }

        private void PostAction()
        {
            GenerateRandomEvent();
            _actionToBill++;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}