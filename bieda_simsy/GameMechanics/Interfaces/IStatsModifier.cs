using bieda_simsy.GameMechanics.Models;

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
        int LiveChanged(Player player);
        int PayForSomething(int money, int cost);
        bool CanAfford(int money, int cost);
        bool IsDead(int life);
        int ClampStat(int value);
    }
}
