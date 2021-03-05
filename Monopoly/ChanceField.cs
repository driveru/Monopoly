using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class ChanceField : GameField
    {
        public ChanceField(int id)
        {
            this.label = "Chance Field";
            this.id = id;
        }
        public override void Action(Player player)
        {
            Random rnd = new Random();
            int x = rnd.Next(11);
            if (x < 5)
            {
                player.PayTaxes(250 * (x + 1));
            }
            else if (x < 10)
            {
                player.ReciveMoney(250 * (x - 4));
            }
            else
            {
                Console.WriteLine($"{player.name} tests a teleportation!");
                player.Move();
            }
        }
    }
}
