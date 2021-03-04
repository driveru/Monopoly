using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class NotEnoughMoneyException : Exception
    {
        public Player player;
        public NotEnoughMoneyException(Player player, string msg)
            :base(msg)
        {
            this.player = player;
        }
    }
}
