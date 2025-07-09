using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using bieda_simsy.Interfaces;

namespace bieda_simsy
{
    internal class SaveManager : SavedInterface
    {
        private static readonly string SaveDirectory = "saves";
        private static readonly string SaveExtension = ".json";
        private readonly PlayerStats _stats = new PlayerStats();

        public SaveManager() : base()
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }
        }

        public Dictionary<string, object> GetData()
        {
            return _stats.GetData();
        }

        public string FileName => $"{SaveDirectory}/{_stats.FileName}{SaveExtension}";

        public void Save()
        {
            var data = GetData();
            var json = JsonSerializer.Serialize(data);
            File.WriteAllText(FileName, json);
        }

        public void LoadData()
        {
            //
        }

        public void DeleteSave()
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
        }
    }
}
