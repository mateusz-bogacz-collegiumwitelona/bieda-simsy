namespace bieda_simsy.GameMechanics.Interfaces
{
    internal interface IStatsModifier
    {
        int AddStats(int stats, int value);
        int OddStats(int stats, int value);
        int AddOddMoney();
        int LiveChanged(int happiness, int hungry, int sleep, int currentLive);
        int PayForSomething(int money, int cost);
        bool CanAfford(int money, int cost);
        bool IsDead(int life);
    }
}
