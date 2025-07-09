using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace bieda_simsy.Interfaces
{
    internal interface SavedInterface
    {
        string FileName { get; }
        Dictionary<string, object> GetData();
        void LoadData();
    }
}
