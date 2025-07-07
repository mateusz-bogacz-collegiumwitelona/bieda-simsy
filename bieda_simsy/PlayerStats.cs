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

        public string SetName()
        {
            Console.Write("Enter your name: ");
            string Name = Console.ReadLine();

            _name = Name;

            return _name;
        }

        public void ShowName()
        {
            Console.WriteLine($"Your name is: {_name}");
        }
    }
}
