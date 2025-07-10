using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bieda_simsy.GameMechanics.Interfaces
{
    internal interface IEvents
    {
        public Dictionary<string, int> GenerateEvent(
            int live,
            int money,
            int happiness,
            int hungry,
            int sleep,
            int purity,
            string name);
    }
}
