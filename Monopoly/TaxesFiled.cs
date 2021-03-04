using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class TaxesFiled : GameField
    {
        public int tax { get; set; }
        public TaxesFiled() { }
        public override void Action(Player player)
        {
            player.PayTaxes(tax);
        }
    }
}
