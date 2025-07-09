using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bieda_simsy.Saved.Models
{
    public class SavedInfo
    {
        public String FileName { get; set; }
        public string PlayerName { get; set; }
        public DateTime SaveDate { get; set; }
        public bool IsAlive { get; set; }
    }
}
