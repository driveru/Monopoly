using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class StartCell : IAction
    {
        public string label { get; set; }
        public int id { get; set; }
        public StartCell()
        {
            label = "Start";
            id = 0;
        }

        public void Action(Player player)
        {          
            Console.WriteLine($"{player.name} enters the start filed!");
            player.ReciveMoney(1000);
        }
        public string GetLabel()
        {
            return label;
        }
    }
}
