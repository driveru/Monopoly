using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class FreeCell : Square
    {
        public FreeCell(int id)
        {
            this.id = id;
            this.label = "Free Cell";
        }
        public override void Action(Player player)
        {
            Console.WriteLine("Nothing happened");
        }
    }
}
