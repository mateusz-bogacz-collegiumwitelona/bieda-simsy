using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bieda_simsy.Saves.Models
{
    /// <summary>
    /// model for appsettings.json 
    /// stores information about save files
    /// </summary>
    internal class SaveSettings
    {
        public string Directory { get; set; } = "saves";
        public string Extension { get; set; } = ".json";
    }
}
