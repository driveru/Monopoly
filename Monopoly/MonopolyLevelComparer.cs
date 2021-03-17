using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Monopoly
{
    
    class MonopolyLevelComparer : IComparer<MonopolyComponent>
    {
        public int Compare([AllowNull] MonopolyComponent x, [AllowNull] MonopolyComponent y)
        {
            return x.GetLevel().CompareTo(y.GetLevel());
        }     
    }
}
