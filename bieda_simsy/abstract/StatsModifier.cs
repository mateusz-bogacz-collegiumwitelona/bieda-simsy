using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bieda_simsy.abstract
{
    public abstract class StatsModifier
    {
    protected int LowHappines(int happiness)
    {
        happiness -= 1;
        return happiness;
    }

    protected int HighHappines(int happiness)
    {
        happiness += 1;
        return happiness;
    }

    protected int LowHungry(int hungry)
    { 
        hungry -= 1;
        return hungry;
    }

    protected int HighHungry(int hungry)
    {
        hungry += 1;
        return hungry;
    }
}
