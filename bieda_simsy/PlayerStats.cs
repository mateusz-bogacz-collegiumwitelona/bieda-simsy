using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bieda_simsy
{
    internal class PlayerStats
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

        protected void GetInfo()
        {
            Console.WriteLine($"Name: {_name}");
            Console.WriteLine($"Live: {_live}");
            Console.WriteLine($"Money: {_money}");
            Console.WriteLine($"Hungry: {_hungry}");
            Console.WriteLine($"Happiness: {_happiness}");
        }

        protected string ShowName
        {
            get { return _name;}
            set { _name = value; }
        }


        protected async Task LowHappines()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromMinutes(1));
                _happiness -= 1;

                if (_happiness <= 0)
                {
                    await Task.Delay(TimeSpan.FromMinutes(1));
                    _live -= 1;

                    if (_live <= 0)
                    {
                        Console.WriteLine("You have died :(.");
                        Environment.Exit(0);
                    }
                }
            }
        }

        protected void HighHappines()
        {
            Console.WriteLine("You played a game and your happiness increased by 1.");
            _happiness += 1;
        }

    }
}
