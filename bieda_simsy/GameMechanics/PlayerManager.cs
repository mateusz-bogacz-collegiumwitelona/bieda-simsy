using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using bieda_simsy.GameMechanics.Abstract;
using bieda_simsy.GameMechanics.RandomEvents;
using bieda_simsy.Saved.Interfaces;

namespace bieda_simsy.GameMechanics
{
    internal class PlayerManager : StatMode, ISaved
    {
        private string _name;
        private int _live;
        private int _money;
        private int _happiness;
        private int _hungry;
        private int _sleep;
        private bool _isAlive;
        private Timer _startTimer;
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

            StartStatsDecay();
        }

        private void StartStatsDecay()
        {
            _startTimer = new Timer(DecayStats, null, 5000, 15000);
        }

        private void DecayStats(object state)
        {
            lock (_lock)
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
        }

        protected string GetName() => _name;

        protected int GetLive() => _live;

        protected int GetMoney() => _money;

        protected int GetHungry() => _hungry;

        protected int GetSleep() => _sleep;

        protected int GetHappiness() => _happiness;

        protected int GetPurity() => _purity;

        protected string SetName()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            _name = name;
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

        private void CheckIfAlive()
        {
            if (_isAlive)
            {
                _startTimer?.Dispose();
                Console.WriteLine($"\n{_name} has died. Game over.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        protected void PlayWith(int value, int choice)
        {
            Console.Clear();
            lock (_lock)
            {
                int oldHappiness = _happiness;
                _happiness = AddStats(_happiness, value);
                int happinessGained = _happiness - oldHappiness;
                Console.WriteLine($"You played with {_name}. Happiness increased by {happinessGained}");
            }
            RandomEvent(choice);
            _actionToBill++;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        protected void Feed(int value, int choice)
        {
            Console.Clear();
            lock (_lock)
            {
                int oldHungry = _hungry;
                _hungry = AddStats(_hungry, value);
                int hungryGained = _hungry - oldHungry;
                Console.WriteLine($"You fed {_name}. Hunger increased by {hungryGained}");
            }
            RandomEvent(choice);
            _actionToBill++;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        protected void YouMustWork(int value, int choice)
        {
            Console.Clear();

            lock (_lock)
            {
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
            }
            RandomEvent(choice);
            _actionToBill++;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        protected void BuySomething(string itemname, int choice, int price, int value, int rand)
        {

            Console.Clear();
            if (CanAfford(_money, price))
            {
                lock (_lock)
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
            }
            else
            {
                Console.WriteLine("Not enough money to buy this item.");
            }
            RandomEvent(rand);
            _actionToBill++;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        protected void Sleep(int value, int choice)
        {
            Console.Clear();
            lock (_lock)
            {
                int oldSleep = _sleep;
                _sleep = AddStats(_sleep, value);
                int sleepGained = _sleep - oldSleep;
                Console.WriteLine($"You slept. Sleep increased by {sleepGained}");
            }
            RandomEvent(choice);
            _actionToBill++;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        protected void WashYourself(int value, int choice)
        {
            Console.Clear();
            lock (_lock)
            {
                int oldPurity = _purity;
                _purity = AddStats(_purity, value);
                int purityGained = _purity - oldPurity;
                Console.WriteLine($"You washed yourself. Purity increased by {purityGained}");
            }
            RandomEvent(choice);
            _actionToBill++;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void Dispose()
        {
            _startTimer?.Dispose();
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
            lock (_lock)
            {
                _name = data["name"]?.ToString() ?? "Unnamed";
                _live = Convert.ToInt32(data["live"]);
                _money = Convert.ToInt32(data["money"]);
                _happiness = Convert.ToInt32(data["happiness"]);
                _hungry = Convert.ToInt32(data["hungry"]);
                _sleep = Convert.ToInt32(data["sleep"]);
                _isAlive = Convert.ToBoolean(data["isAlive"]);
            }
        }

        public Dictionary<string, object> GetData()
        {
            lock (_lock)
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
        }

        protected void RandomEvent(int choice)
        {
            PositiveEvents positiveEvents = new PositiveEvents();
            NegativeEvents negativeEvents = new NegativeEvents();

            Dictionary<string, int> eventResults;

            switch (choice)
            {
                case 1:
                    eventResults = positiveEvents.GeneratePositiveEvent(_live, _money, _happiness, _hungry, _sleep, _purity, _name);
                    ApplyEventsResoult(eventResults);
                    break;
                case 2:
                    eventResults = negativeEvents.GenerateNegativeEvent(_live, _money, _happiness, _hungry, _sleep, _purity, _name);
                    ApplyEventsResoult(eventResults);
                    break;
                default:
                    return;
            }
        }

        private void ApplyEventsResoult(Dictionary<string, int> eventResults)
        {
            lock (_lock)
            {
                if (eventResults.ContainsKey("live"))
                    _live = eventResults["live"];
                if (eventResults.ContainsKey("money"))
                    _money = eventResults["money"];
                if (eventResults.ContainsKey("happiness"))
                    _happiness = eventResults["happiness"];
                if (eventResults.ContainsKey("hungry"))
                    _hungry = eventResults["hungry"];
                if (eventResults.ContainsKey("sleep"))
                    _sleep = eventResults["sleep"];
                if (eventResults.ContainsKey("purity"))
                    _purity = eventResults["purity"];
            }
        }
    }
}