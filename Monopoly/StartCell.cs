using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class StartCell : Square
    {
        public StartCell()
        {
            label = "Start";
            id = 0;
        }
        public override void Action(Player player)
        {          
            Console.WriteLine($"{player.name} enters the start filed!");
            player.ReciveMoney(1000);
        }
    }
}
