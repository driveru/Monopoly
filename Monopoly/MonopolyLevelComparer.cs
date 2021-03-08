using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Monopoly
{
    class MonopolyLevelComparer : IComparer<EstateField>
    {
        public int Compare([AllowNull] EstateField x, [AllowNull] EstateField y)
        {
            return x.monopoly_level.CompareTo(y.monopoly_level);
        }
    }
}
