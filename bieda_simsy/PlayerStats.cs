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
        private bool _isAlive;
        private Timer _startTimer;
        private readonly object _lock = new object();
        private int _sleep;

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
            _startTimer = new Timer(DecayStats, null, 15000, 15000);
        }

        private void DecayStats(object state)
        {
            lock (_lock)
            {
                if (!_isAlive) return;

                Random random = new Random();

                _happiness = Math.Max(0, _happiness - random.Next(1,4));
                _hungry = Math.Max(0, _hungry - random.Next(1, 4));
                _sleep = Math.Max(0, _sleep - random.Next(1, 4));

                UpdateLifeBasedOnStats();


            }
        }

        private void UpdateLifeBasedOnStats()
        {
            Random random = new Random();

            if (_happiness <= 0 || _hungry <= 0 || _sleep <= 0 )
            { 
                int lifeLoss = random.Next(1, 5);

                if (_happiness <=0 && _hungry <= 0)
                {
                    lifeLoss *= 2;
                }

                if (_happiness <= 0 && _hungry <= 0 && _sleep <= 0)
                {
                    lifeLoss *= 3;
                }

                _live = Math.Max(0, _live - lifeLoss);
            }
            else if (_happiness >= 100 && _hungry >= 100 && _sleep >= 100)
            {
                _live = Math.Min(100, _live + random.Next(1, 5));
            }
        }

        private void CheckIfAlive()
        {
            if (_live <= 0)
            {
                _isAlive = false;
                _startTimer?.Dispose();
                Console.WriteLine($"\n{_name} has died. Game over.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        public bool IsAlive => _isAlive;

        protected string SetName()
        {
            Console.Write("Enter your name: ");
            string Name = Console.ReadLine();

            _name = Name;

            return _name;
        }

        protected string ShowName
        {
            get { return _name; }
            set { _name = value; }
        }

        protected void GetInfo()
        {
            Console.WriteLine($"Name: {_name}");
            Console.WriteLine($"Live: {_live}");
            Console.WriteLine($"Money: {_money}");
            Console.WriteLine($"Hungry: {_hungry}");
            Console.WriteLine($"Happiness: {_happiness}");
            Console.WriteLine($"Sleep: {_sleep}");

            if (_happiness < 30) Console.WriteLine("Very unhappy!");
            if (_hungry < 30) Console.WriteLine("Very hungry!");
            if (_live < 30) Console.WriteLine("Critical health!");
            if (_sleep < 30) Console.WriteLine("Critical sleep!");
        }

        protected void GetMoney()
        {
            Console.WriteLine($"Money: {_money}");
        }

        protected void PlayWith()
        {
            Console.Clear();
            int happy = AddHappines(_happiness);
            Console.WriteLine($"You played with {_name}. Hapinest decreased by {happy}");
            lock (_lock) _happiness = happy;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }

        protected void Feed()
        {
            Console.Clear();
            int hungry = AddHungry(_hungry);
            Console.WriteLine($"You fed {_name}. Hunger decreased by {hungry}");
            lock (_lock) _hungry = hungry;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        protected void YouMustWork()
        {
            Console.Clear();

            _happiness = OddHappiness(_happiness);
            _hungry = OddHungry(_hungry);
            _money = Work(_money);
            _sleep = OddSleep(_sleep);

            Console.WriteLine($"You worked and earned some money. Current money: {_money}");
            Console.WriteLine($"But work is boring: You have {_happiness} happiness, {_hungry} hunger and {_sleep} sleep.");;

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        protected void BuySomeFood()
        {
            lock (_lock)
            {
                if (_money >= 10)
                {
                    _money = OddMoney(_money);
                    _hungry = AddHungry(_hungry);
                    Console.WriteLine("You bought some food.");
                }
                else
                {
                    Console.WriteLine("Not enough money to buy food.");
                }
            }
        }

        protected void BuyAToy()
        {
            lock (_lock)
            {
                if (_money >= 10)
                {
                    _money = OddMoney(_money);
                    _happiness = AddHappines(_happiness);
                    Console.WriteLine("You bought a toy.");
                }
                else
                {
                    Console.WriteLine("Not enough money to buy a toy.");
                }
            }
        }

        protected void Sleep()
        {
            Console.Clear();
            int sleep = AddSleep(_sleep);
            Console.WriteLine($"You slept well. Sleep increased by {sleep}");
            lock (_lock) _sleep = sleep;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void Dispose()
        {
            _startTimer?.Dispose();
        }
    }
}