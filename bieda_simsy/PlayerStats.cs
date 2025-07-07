using System;
using System.Collections.Generic;
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

        public PlayerStats()
        {
            _name = string.Empty;
            _live = 100;
            _money = 10;
            _happiness = 100;
            _hungry = 100;
        }

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
        }

        protected void GetMoney()
        {
            Console.WriteLine($"Money: {_money}");
        }

        protected void PlayWith()
        {
            _happiness = AddHappines(_happiness);
        }

        protected void Feed()
        {
            _hungry = AddHungry(_hungry);
        }

        protected void Health()
        {
            _live = AddLive(_happiness, _hungry, _live);
        }

        protected void YouMustWork()
        {
            int oddHappy = OddHappiness(_happiness);
            int oddFood = OddHungry(_hungry);
            int money = Work(_money);

            Console.WriteLine($"You worked and earned some money. Current money: {money}");
            Console.WriteLine($"But work is boring: You have {oddHappy} happiness and {oddFood} hunger.");
            _happiness = oddHappy;
            _hungry = oddFood;
            _money += money;

        }

        protected void BuySomeFood()
        {
            _money = OddMoney(_money);
            _hungry = BuyFood(_hungry);
        }

        protected void BuyAToy()
        {
            _money = OddMoney(_money);
            _happiness = BuyHappiness(_happiness);
        }


    }
}