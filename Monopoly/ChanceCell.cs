using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class ChanceCell : Square
    {
        public ChanceCell(int id)
        {
            this.label = "Chance Field";
            this.id = id;
        }
        public override void Action(Player player)
        {
            Random rnd = new Random();
            int x = rnd.Next(12);
            if (x < 5)
            {
                player.PayTaxes(250 * (x + 1));
            }
            else if (x < 10)
            {
                player.ReciveMoney(250 * (x - 4));
            }
            else if (x < 11)
            {
                Console.WriteLine($"{player.name} tests a teleportation!");
                player.Move();
            }
            else
            {
                Console.WriteLine($"{player.name} takes a nap!");
                player.BlockMove(1);
            }
        }
    }
}
