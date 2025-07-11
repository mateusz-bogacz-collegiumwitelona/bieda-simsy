namespace bieda_simsy.GameMechanics.Interfaces
{
    /// <summary>
    /// interface to stats modifier functions
    /// </summary>
    internal interface IStatsModifier
    {
        int AddStats(int stats, int value);
        int OddStats(int stats, int value);
        int AddOddMoney();
        int LiveChanged(int happiness, int hungry, int sleep, int currentLive, int purity);
        int PayForSomething(int money, int cost);
        bool CanAfford(int money, int cost);
        bool IsDead(int life);
    }
}
