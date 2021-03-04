using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException()
            :base() { }
    }
}
