using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Monopoly
{
    class MonopolyLevelComparer : IComparer<EstateCell>
    {
        public int Compare([AllowNull] EstateCell x, [AllowNull] EstateCell y)
        {
            return x.monopoly_level.CompareTo(y.monopoly_level);
        }
    }
}
