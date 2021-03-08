using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class JailField : GameField
    {
        public JailField()
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
