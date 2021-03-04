using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class Player
    {
        string name { get; set; }
        List<EstateField> estates { get; set; }
        int money { get; set; }
        int current_position { get; set; }
        public Player(string name)
        {
            this.name = name;
            this.money = 15000;
            current_position = 0;
            estates = new List<EstateField>();
        }

        private void Pay(Player other_player, int amount)
        {
            other_player.money += amount;
        }

        private int Move()
        {
            Random rnd = new Random();
            int step = rnd.Next(2, 13);
            current_position += step;
            return step;
        }
        public void Buy(EstateField estate)
        {
            if (estate.price > money)
            {
                throw new NotEnoughMoneyException();
            }
            else
            {
                money -= estate.price;
                estates.Add(estate);
            }
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Player: {name}, money: {money}");
            Console.WriteLine("Estates:");
            foreach (EstateField estate in estates)
            {
                Console.WriteLine(estate);
            }
            Console.WriteLine("-----------------------------------------------");
        }
    }
}
