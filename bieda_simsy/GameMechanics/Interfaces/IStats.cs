namespace bieda_simsy.GameMechanics.Interfaces
{
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
