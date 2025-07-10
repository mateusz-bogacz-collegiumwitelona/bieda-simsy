namespace bieda_simsy.Saved.Interfaces
{
    internal interface ISaved
    {
        string FileName { get; }
        Dictionary<string, object> GetData();
        void LoadData(Dictionary<string, object> data);
    }
}
