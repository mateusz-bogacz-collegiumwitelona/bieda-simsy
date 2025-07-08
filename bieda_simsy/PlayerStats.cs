using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using bieda_simsy.@abstract;

namespace bieda_simsy
{
    internal class PlayerStats : StatMode
    {
        private string _name;
        private int _live;
        private int _money;
        private int _happiness;
        private int _hungry;
        private int _sleep;
        private bool _isAlive;
        private Timer _startTimer;
        private readonly object _lock = new object();

        public bool IsAlive => _isAlive;

        public PlayerStats()
        {
            _name = string.Empty;
            _live = 100;
            _money = 10;
            _happiness = 100;
            _hungry = 100;
            _isAlive = true;
            _sleep = 100;

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
                
                _happiness = NaturalOddHappiness(_happiness);
                _hungry = NaturalOddHungry(_hungry);
                _sleep = NaturalOddSleep(_sleep);

                _live = LiveChanged(_happiness, _hungry, _sleep, _live);

                if (IsDead(_live))
                {
                    _isAlive = false;
                    CheckIfAlive();
                }
            }
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

        protected string SetName()
        {
            Console.Write("Enter your name: ");
            string Name = Console.ReadLine();
            _name = Name;
            return _name;
        }

        protected void GetInfo()
        {
            Console.WriteLine($"Name: {_name}");
            Console.WriteLine($"Live: {_live}");
            Console.WriteLine($"Money: {_money}");
            Console.WriteLine($"Hungry: {_hungry}");
            Console.WriteLine($"Happiness: {_happiness}");
            Console.WriteLine($"Sleep: {_sleep}");

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

        protected void PlayWith()
        {
            Console.Clear();
            lock (_lock)
            {
                int oldHappiness = _happiness;
                _happiness = AddHappines(_happiness);
                int happinessGained = _happiness - oldHappiness;
                Console.WriteLine($"You played with {_name}. Happiness increased by {happinessGained}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        protected void Feed()
        {
            Console.Clear();
            lock (_lock)
            {
                int oldHungry = _hungry;
                _hungry = AddHungry(_hungry);
                int hungryGained = _hungry - oldHungry;
                Console.WriteLine($"You fed {_name}. Hunger increased by {hungryGained}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        protected void YouMustWork()
        {
            Console.Clear();

            lock (_lock)
            {
                int moneyFromWork = MoneyFromWork();
                _money += moneyFromWork;

                _happiness = OddHappiness(_happiness);
                _hungry = OddHungry(_hungry);
                _sleep = OddSleep(_sleep);

                Console.WriteLine($"You worked and earned {moneyFromWork} money. Current money: {_money}");
                Console.WriteLine($"But work is boring: You have {_happiness} happiness, {_hungry} hunger and {_sleep} sleep.");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        protected void BuySomeFood()
        {
            Console.Clear();
            
            lock (_lock)
            {
                if(CanAfford(_money, 10))
                {
                    _money = ProcessPurchase(_money, 10);
                    _hungry = AddHungry(_hungry);
                    Console.WriteLine($"You bought some food.  {_name} current have {_hungry} pkt of hungry");
                }
                else
                {
                    Console.WriteLine("Not enough money to buy food.");
                }
            }
            
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        protected void BuyAToy()
        {
            Console.Clear();

            lock (_lock)
            {
                if(CanAfford(_money, 10))
                {
                    _money = ProcessPurchase(_money, 10);
                    _happiness = AddHappines(_happiness);
                    Console.WriteLine($"You bought a toy. {_name} current have {_happiness} pkt of happiness");
                }
                else
                {
                    Console.WriteLine("Not enough money to buy a toy.");
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        protected void BuyCoffe()
        {
            Console.Clear();
            lock (_lock)
            {
                if(CanAfford(_money, 5))
                {
                    _money = ProcessPurchase(_money, 5);
                    _sleep = AddSleep(_sleep);
                    Console.WriteLine($"You bought a coffee. {_name} current have {_sleep} pkt of sleepness");
                }
                else
                {
                    Console.WriteLine("Not enough money to buy coffee.");
                }
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
                _sleep = AddSleep(_sleep);
                int sleepGained = _sleep - oldSleep;
                Console.WriteLine($"You slept. Sleep increased by {sleepGained}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void Dispose()
        {
            _startTimer?.Dispose();
        }
    }
}