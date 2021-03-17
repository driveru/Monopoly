﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class TaxesCell : IAction
    {
        public int tax { get; set; }
        public string label { get; set; }
        public int id { get; set; }

        public TaxesCell() { }
        public void Action(Player player)
        {
            player.PayTaxes(tax);
        }
        public string GetLabel()
        {
            return label;
        }
    }
}
