using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    interface IAction
    {
        string label { get; set; }
        int id { get; set; }
        void Action(Player player);
    }
}
