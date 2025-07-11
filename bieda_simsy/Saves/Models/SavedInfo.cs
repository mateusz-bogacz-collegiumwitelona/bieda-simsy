namespace bieda_simsy.Saved.Models
{
    /// <summary>
    /// game save information for the save list
    /// </summary>
    public class SavedInfo
    {
        public String FileName { get; set; }
        public string PlayerName { get; set; }
        public DateTime SaveDate { get; set; }
        public bool IsAlive { get; set; }
    }
}
