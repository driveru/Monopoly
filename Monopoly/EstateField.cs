using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class EstateField : GameField
    {
        public bool IsBought { get; set; }
        public int price { get; set; }
        public Player owner { get; set; }
        public int rent { get; set; }
        public EstateField() { }
        public override string ToString()
        {
            return $"{id}) {label}, price: {price}, rent:{rent}";
        }
    }
}
