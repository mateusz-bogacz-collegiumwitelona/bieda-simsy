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

        protected void SetMinusStats()
        {
            _live = OddLive(_live, _happiness, _hungry);
            _hungry = OddHungry(_hungry);
            _happiness = OddHappiness(_happiness);
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
    }
}