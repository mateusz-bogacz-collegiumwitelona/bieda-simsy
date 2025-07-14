using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bieda_simsy.GameMechanics.Models
{
    internal class PlayerDefaults
    {
        public string Name { get; set; } = "";
        public int Live { get; set; } = 100;
        public int Money { get; set; } = 10;
        public int Happiness { get; set; } = 100;
        public int Hungry { get; set; } = 100;
        public int Sleep { get; set; } = 100;
        public int Purity { get; set; } = 100;
        public bool IsAlive { get; set; } = true;

    }
}
