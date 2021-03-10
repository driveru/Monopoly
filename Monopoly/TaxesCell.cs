using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class TaxesCell : Square
    {
        public int tax { get; set; }
        public TaxesCell() { }
        public override void Action(Player player)
        {
            player.PayTaxes(tax);
        }
    }
}
