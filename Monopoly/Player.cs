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
            {
                money += Sell(amount - this.money);
            }
            Console.WriteLine($"{this.name} paid {amount}$ to {other_player.name}!");
            other_player.ReciveMoney(amount);
            this.money -= amount;        
        }
        public void PayTaxes(int tax_amount)
        {
            if (this.money < tax_amount)
            {
                money += Sell(tax_amount - this.money);
            }
            money -= tax_amount;
            Console.WriteLine($"{this.name} paid {tax_amount}$ to treasury!");           
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
        private int Sell(int need_money)
        {
            List<EstateField> estates_for_sale = FindEstatesToSell(need_money);
            double got_money = 0;
            foreach (EstateField estate in estates_for_sale)
            {
                estate.IsBought = false;
                estate.owner = null;
                got_money += Math.Round(estate.price * 0.7);               
                this.estates.Remove(estate);
            }
            return (int)got_money;
        }
        public void ReciveMoney(int got_money)
        {
            Console.WriteLine($"{this.name} got {got_money}$ !");
            money += got_money;
        }
        private List<EstateField> FindEstatesToSell(int need_money)
        {
            List<EstateField> estates_to_sell = new List<EstateField>();
            double sum = 0;
            foreach (EstateField estate in estates)
            {
                if (sum < need_money)
                {
                    sum += Math.Round(estate.price * 0.7);
                    estates_to_sell.Add(estate);
                }
            }
            if (sum < need_money)
            {
                throw new NotEnoughMoneyException(this, $"{this.name} haven't enough estates to sell to pay.");
            }
            return estates_to_sell;
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
            Console.WriteLine("----------------------------------------------------------");
        }
    }
}
