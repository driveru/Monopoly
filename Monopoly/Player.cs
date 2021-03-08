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
        int movement_block { get; set; }
        bool jailed { get; set; }
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{this.name} paid {amount}$ to {other_player.name}!");
            Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{this.name} paid {tax_amount}$ to treasury!");
            Console.ResetColor();
        }
        public void Move()
        {
            Random rnd = new Random();
            int first_die = rnd.Next(1, 7);
            int second_die = rnd.Next(1, 7);
            if (jailed && (first_die == second_die))
            {
                movement_block = 0;
                jailed = false;
            } 

            if (movement_block > 0)
            {
                if (jailed && movement_block == 1)
                {
                    Console.WriteLine($"{this.name} must pay 500$ to leave the jail!");
                    this.PayTaxes(500);
                    jailed = false;
                }
                movement_block--;
            }
            else
            {              
                int step = first_die + second_die;
                Console.WriteLine($"Rolled: {first_die} and {second_die}!");
                if (current_position + step >= 40)
                {
                    if (current_position + step == 40)
                    {
                        Console.WriteLine($"{this.name} enters the start filed!");
                        this.ReciveMoney(1000);
                    }
                    Console.WriteLine($"{this.name} pass the entire game field!");
                    this.ReciveMoney(2000);
                }
                current_position = (current_position + step) % 40;

                if (Program.map[current_position] != null)
                {
                    Console.WriteLine($"{this.name} enters the {Program.map[current_position].label} field!");
                    Program.map[current_position].Action(this);
                }
                else
                {
                    Console.WriteLine("Unknown Field");
                }
                BuildMonopoly();
                if (first_die == second_die)
                    this.Move();
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
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{this.name} bought {estate.label}");
                Console.ResetColor();
                if (IsMonopoly(estate))
                    estate.monopolyINFO.UpToMonopoly();                  
            }
        }
        private int Sell(int need_money)
        {
            List<EstateField> estates_for_sale = FindEstatesToSell(need_money);
            int got_money = 0;
            foreach (EstateField estate in estates_for_sale)
            {
                estate.IsBought = false;
                estate.owner = null;
                got_money += (int)Math.Round(estate.price * 0.7);               
                this.estates.Remove(estate);
            }
            if (got_money >= need_money)
            {
                return got_money;
            }
            else
            {
                got_money += SellMonopoly(need_money - got_money);
            }
            return got_money;
        }
        public void ReciveMoney(int got_money)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{this.name} got {got_money}$ !");
            Console.ResetColor();
            money += got_money;
        }
        public void GoToJail()
        {
            jailed = true;
            movement_block = 3;
            current_position = 10;
        }
        private void BuildMonopoly()
        {
            List<MonopolyINFO> monopolies = new List<MonopolyINFO>(); 
            foreach (EstateField estate in this.estates)
            {
                if (estate.monopoly_level > 0)
                {
                    if (!monopolies.Contains(estate.monopolyINFO))
                        monopolies.Add(estate.monopolyINFO);
                }
            }
            foreach (MonopolyINFO monopoly in monopolies)
            {
                if (monopoly.house_price <= this.money)
                {
                    monopoly.BuildField();
                    this.money -= monopoly.house_price;
                }
            }
        }
        private bool IsMonopoly(EstateField estate)
        {
            foreach (EstateField monopoly_estate in estate.monopolyINFO.monopoly_estates)
            {
                if (!this.estates.Contains(monopoly_estate))
                {
                    return false;
                }
            }
            return true;
        }
        private List<EstateField> FindEstatesToSell(int need_money)
        {
            List<EstateField> estates_to_sell = new List<EstateField>();
            double sum = 0;
            estates.Sort(new MonopolyLevelComparer());
            foreach (EstateField estate in estates)
            {
                if (estate.monopoly_level == 0)
                {
                    if (sum < need_money)
                    {
                        sum += Math.Round(estate.price * 0.7);
                        estates_to_sell.Add(estate);
                    }
                }
                else
                {
                    break;
                }
            }
            estates.Sort();
            return estates_to_sell;
        }
        private int SellMonopoly(int need_money)
        {
            int got_money = 0;
            while ((estates.Count > 0) && (got_money < need_money))
            {
                int money_for_sale = estates[0].monopolyINFO.SellHouse();
                if (money_for_sale == 0)
                {
                    got_money += Sell(need_money - got_money);
                }
                else
                {
                    got_money += money_for_sale;
                }
            }
            if (got_money < need_money)
            {
                throw new NotEnoughMoneyException(this, "smth");
            }
            return got_money;
        }
        public void PrintInfo()
        {
            Console.WriteLine($"Player: {name}, money: {money}, current position: {current_position}");
            Console.WriteLine("Estates:");
            estates.Sort();
            foreach (EstateField estate in estates)
            {
                Console.ForegroundColor = EstateField.colors[estate.monopoly_key - 1];
                Console.WriteLine(estate);
                Console.ResetColor();
            }
            Console.WriteLine("----------------------------------------------------------");
        }
    }
}
