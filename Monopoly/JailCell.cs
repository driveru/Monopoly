using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class JailCell : Square
    {
        public JailCell()
        {
            label = "Jail";
            id = 30;
        }
        public override void Action(Player player)
        {
            player.GoToJail();
        }
    }
}
