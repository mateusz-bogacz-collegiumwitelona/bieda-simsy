using bieda_simsy.GameMechanics.Models;
using bieda_simsy.Saves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bieda_simsy.GameMechanics.Settings
{
    internal class AppSettings
    {
        public SaveSettings SaveSettings { get; set; }
        public PlayerDefaults PlayerDefaults { get; set; }
    }
}
