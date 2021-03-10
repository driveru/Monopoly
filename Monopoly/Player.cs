using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class Player
    {
        private Die[] dice { get; set; }
        public string name { get; set; }
        private List<EstateCell> estates { get; set; }
        private int money { get; set; }
        private IAction current_position { get; set; }
        private int movement_block { get; set; }
        private bool jailed { get; set; }
        private int score { get; set; }
        public Player(string name)
        {
            this.name = name;
            this.money = 15000;
            current_position = Game.board.GetSquare(0);
            estates = new List<EstateCell>();
            dice = new[] { new Die(), new Die() };
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
            other_player.IncreaseScore(amount);
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
            // roll dice
            foreach (Die die in dice)
            {
                die.roll();
            }

            if (jailed && (dice[0] == dice[1]))
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
                // calculate total step
                int step = 0;
                foreach(Die die in dice)
                {
                    step += die.faceValue;
                }

                Console.WriteLine($"Rolled: {dice[0]} and {dice[1]}!");
                if (current_position.id + step >= 40)
                {
                    Console.WriteLine($"{this.name} pass the entire game field!");
                    this.ReciveMoney(2000);
                }

                current_position = Game.board.GetSquare((current_position.id + step) % 40);

                Console.WriteLine($"{this.name} enters the {current_position.label} field!");
                current_position.Action(this);
                BuildMonopoly();

                if (dice[0].faceValue == dice[1].faceValue)
                    this.Move();
            }
        }
        public void Buy(EstateCell estate)
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
            List<EstateCell> estates_for_sale = FindEstatesToSell(need_money);
            int got_money = 0;
            foreach (EstateCell estate in estates_for_sale)
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
            current_position = Game.board.GetSquare(10);
        }
        private void BuildMonopoly()
        {
            List<MonopolyINFO> monopolies = new List<MonopolyINFO>(); 
            foreach (EstateCell estate in this.estates)
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
        private bool IsMonopoly(EstateCell estate)
        {
            foreach (EstateCell monopoly_estate in estate.monopolyINFO.monopoly_estates)
            {
                if (!this.estates.Contains(monopoly_estate))
                {
                    return false;
                }
            }
            return true;
        }
        private List<EstateCell> FindEstatesToSell(int need_money)
        {
            List<EstateCell> estates_to_sell = new List<EstateCell>();
            double sum = 0;
            estates.Sort(new MonopolyLevelComparer());
            foreach (EstateCell estate in estates)
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
            Console.WriteLine($"Player: {name}, money: {money}, current position: {current_position.id}, score: {score}");
            Console.WriteLine("Estates:");
            estates.Sort();
            foreach (EstateCell estate in estates)
            {
                Console.ForegroundColor = EstateCell.colors[estate.monopoly_key - 1];
                Console.WriteLine(estate);
                Console.ResetColor();
            }
            Console.WriteLine("--------------------------------------------------------------------");
        }
        public void IncreaseScore(int val)
        {
            score += val;
        }
        public void BlockMove(int round_cnt)
        {
            movement_block = round_cnt;
        }
    }
}
