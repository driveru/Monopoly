using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class Player
    {
        public string name { get; set; }
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

        public void PayRent(Player other_player, int amount)
        {
            if (this.money < amount)
                    throw new NotEnoughMoneyException(this, $"{this.name} can't pay {amount}$ to {other_player.name}");
            else
            {
                other_player.money += amount;
                this.money -= amount;
                Console.WriteLine($"{this.name} paid {amount}$ to {other_player.name}!");
            }
            
        }
        public void PayTaxes(int tax_amount)
        {
            if (this.money < tax_amount)
            {
                throw new NotEnoughMoneyException(this, $"{this.name} can't pay taxes on {tax_amount}$");
            }
            else
            {
                Console.WriteLine($"{this.name} paid {tax_amount}$ to treasury!");
            }
        }
        public void Move()
        {
            Random rnd = new Random();
            int step = rnd.Next(2, 13);
            current_position = (current_position + step) % 40;
            Console.WriteLine($"Rolled: {step}!");           
            if (Program.map[current_position] != null)
            {
                Console.WriteLine($"{this.name} enters the {Program.map[current_position].label} field!");
                Program.map[current_position].Action(this);
            }
            else
            {
                Console.WriteLine("Unknown Field");
            }
        }
        public void Buy(EstateField estate)
        {
            if (estate.price > money)
            {
                Console.WriteLine($"{this.name} haven't enough money to buy {estate.label}");
            }
            else
            {
                money -= estate.price;
                estates.Add(estate);
                estate.IsBought = true;
                estate.owner = this;
                Console.WriteLine($"{this.name} bought {estate.label}");
            }
        }


        public void PrintInfo()
        {
            Console.WriteLine($"Player: {name}, money: {money}, current position: {current_position}");
            Console.WriteLine("Estates:");
            estates.Sort();
            foreach (EstateField estate in estates)
            {
                Console.WriteLine(estate);
            }
            Console.WriteLine("-----------------------------------------------");
        }
    }
}
