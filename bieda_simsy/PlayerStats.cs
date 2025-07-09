using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using bieda_simsy.@abstract;
using bieda_simsy.Interfaces;

namespace bieda_simsy
{
    internal class PlayerStats : StatMode, ISaved
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

        public bool IsAlive => _isAlive;
        public string FileName => 
            string.IsNullOrEmpty(_name) 
            ? "default_save" : _name.ToLower().Replace(" ", "_");

        public PlayerStats()
        {
            _name = string.Empty;
            _live = 100;
            _money = 10;
            _happiness = 100;
            _hungry = 100;
            _isAlive = true;
            _sleep = 100;
            _actionToBill = 0;

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


                _happiness = NaturalOddStats(_happiness, happinessValue);
                _hungry = NaturalOddStats(_hungry, hungryValue);
                _sleep = NaturalOddStats(_sleep, sleepValue);

                _live = LiveChanged(_happiness, _hungry, _sleep, _live);

                if (IsDead(_live))
                {
                    _isAlive = false;
                    CheckIfAlive();
                }
            }
        }
        
        protected string GetName()
        {
            return _name;
        }

        protected int GetLive()
        {
            return _live;
        }

        protected int GetMoney()
        {
            return _money;
        }

        protected int GetHungry()
        {
            return _hungry;
        }

        protected int GetSleep()
        {
            return _sleep;
        }

        protected int GetHappiness()
        {
            return _happiness;
        }

        protected string SetName()
        {
            Console.Write("Enter your name: ");
            string Name = Console.ReadLine();
            _name = Name;
            return _name;
        }

        private void CheckIfAlive()
        {
            if(_isAlive)
            {
                _startTimer?.Dispose();
                Console.WriteLine($"\n{_name} has died. Game over.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        protected void PlayWith(int value)
        {
            Console.Clear();
            lock (_lock)
            {
                int oldHappiness = _happiness;
                _happiness = AddStats(_happiness, value);
                int happinessGained = _happiness - oldHappiness;
                Console.WriteLine($"You played with {_name}. Happiness increased by {happinessGained}");
            }
            _actionToBill++;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        protected void Feed(int value)
        {
            Console.Clear();
            lock (_lock)
            {
                int oldHungry = _hungry;
                _hungry = AddStats(_hungry, value);
                int hungryGained = _hungry - oldHungry;
                Console.WriteLine($"You fed {_name}. Hunger increased by {hungryGained}");
            }
            _actionToBill++;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        protected void YouMustWork(int value)
        {
            Console.Clear();

            lock (_lock)
            {
                int moneyFromWork = AddOddMoney();
                _money += moneyFromWork;

                _happiness = OddStats(_happiness, value);
                _hungry = OddStats(_hungry, value);
                _sleep = OddStats(_sleep, value);

                Console.WriteLine($"You worked and earned {moneyFromWork} money. Current money: {_money}");
                Console.WriteLine($"But work is boring: You have {_happiness} happiness, {_hungry} hunger and {_sleep} sleep.");
            }
            _actionToBill++;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        protected void BuySomething(string itemname, int choice, int price, int value)
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
                            Console.WriteLine($"You bought {itemname}. {_name} current have {_hungry} pkt of hungry");
                            break;
                        case 2:
                            _happiness = AddStats(_happiness, value);
                            Console.WriteLine($"You bought {itemname}. {_name} current have {_happiness} pkt of happiness");
                            break;
                        case 3:
                            _sleep = AddStats(_sleep, value);
                            Console.WriteLine($"You bought {itemname}. {_name} current have {_sleep} pkt of sleepness");
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
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
       
        protected void Sleep()
        {
            Console.Clear();
            lock (_lock)
            {
                int oldSleep = _sleep;
                _sleep = AddStats(_sleep, 20);
                int sleepGained = _sleep - oldSleep;
                Console.WriteLine($"You slept. Sleep increased by {sleepGained}");
            }
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
                { "isAlive", _isAlive }
            };
        }
    }
}