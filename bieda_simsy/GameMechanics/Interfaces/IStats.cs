namespace bieda_simsy.GameMechanics.Interfaces
{
    /// <summary>
    /// interface storing the definitions of getters and setters of ststats
    /// </summary>
    internal interface IStats
    {
        string Name { get; set; }
        int Live { get; }
        int Money { get; }
        int Hungry { get; }
        int Sleep { get; }
        int Happiness { get; }
        int Purity { get; }
        
    }
}
