using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class GoToJailCell : IAction
    {
        public string label { get; set; }
        public int id { get; set; }
        public GoToJailCell()
        {
            label = "Jail";
            id = 30;
        }
        public void Action(Player player)
        {
            player.GoToJail();
        }
        public string GetLabel()
        {
            return label;
        }
    }
}
