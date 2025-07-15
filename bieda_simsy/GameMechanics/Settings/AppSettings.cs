using bieda_simsy.GameMechanics.Models;
using bieda_simsy.Saves.Models;

namespace bieda_simsy.GameMechanics.Settings
{
    /// <summary>
    /// model to implements SaveSettings and PlayerDefaults
    /// </summary>
    internal class AppSettings
    {
        public SaveSettings SaveSettings { get; set; }
        public PlayerDefaults PlayerDefaults { get; set; }
    }
}
