namespace bieda_simsy.Saved.Interfaces
{
    /// <summary>
    /// interface for objects that can be saved and loaded
    /// </summary>
    internal interface ISaved
    {
        string FileName { get; }
        Dictionary<string, object> GetData();
        void LoadData(Dictionary<string, object> data);
    }
}
