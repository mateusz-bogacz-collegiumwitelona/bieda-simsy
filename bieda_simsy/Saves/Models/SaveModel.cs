using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bieda_simsy.Saved.Models
{
    public class SaveModel
    {
        public string Name { get; set; }
        public int Live { get; set; }
        public int Money { get; set; }
        public int Happiness { get; set; }
        public int Hungry { get; set; }
        public int Sleep { get; set; }
        public int Purity { get; set; }
        public bool IsAlive { get; set; }
        public DateTime SaveDate { get; set; }

        public SaveModel()
        {
            SaveDate = DateTime.Now;
        }

    }
}
