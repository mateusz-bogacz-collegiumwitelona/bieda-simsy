using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace bieda_simsy.Saved.Interfaces
{
    internal interface ISaved
    {
        string FileName { get; }
        Dictionary<string, object> GetData();
        void LoadData(Dictionary<string, object> data);
    }
}
