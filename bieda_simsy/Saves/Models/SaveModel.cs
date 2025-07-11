namespace bieda_simsy.Saved.Models
{
    /// <summary>
    /// data model for saving the game state
    /// </summary>
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
