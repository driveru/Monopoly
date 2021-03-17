using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class Player
    {
        private Die[] dice { get; set; }
        public string name { get; set; }
        private List<int> estate_ids { get; set; }
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
            estate_ids = new List<int>();
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

                current_position = Game.board.GetSquare(current_position, step);

                Console.WriteLine($"{this.name} enters the {(current_position).GetLabel()} field!");
                current_position.Action(this);
                BuildMonopoly();

                if (dice[0].faceValue == dice[1].faceValue)
                    this.Move();
            }
        }
        
        public void Buy(EstateCell estate)
        {
            if (estate.Buy(this, this.money))
            {
                money -= estate.GetPrice();
                estate_ids.Add(estate.id);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{this.name} bought {estate.GetLabel()}");
                Console.ResetColor();
                if (IsMonopoly(estate))
                    estate.GetMonopolyINFO().UpToMonopoly();               
            }
            else
            {
                Console.WriteLine($"{this.name} haven't enough money to buy {estate.GetLabel()}");
            }
        }
        
        private int Sell(int need_money)
        {
            List<int> estate_ids_for_sale = FindEstatesToSell(need_money);
            int got_money = 0;
            foreach (int estate_id in estate_ids_for_sale)
            { 
                got_money += (Game.board.GetSquare(estate_id) as MonopolyComponent).Sell();               
                this.estate_ids.Remove(estate_id);
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
            foreach (int estate_id in this.estate_ids)
            {
                if ((Game.board.GetSquare(estate_id) as MonopolyComponent).GetLevel() > 0)
                {
                    if (!monopolies.Contains((Game.board.GetSquare(estate_id) as MonopolyComponent).GetMonopolyINFO()))
                        monopolies.Add((Game.board.GetSquare(estate_id) as MonopolyComponent).GetMonopolyINFO());
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
            foreach (int monopoly_estate in estate.GetMonopolyINFO().monopoly_estates_ids)
            {
                if (!this.estate_ids.Contains(monopoly_estate))
                {
                    return false;
                }
            }
            return true;
        }
       
        private List<int> FindEstatesToSell(int need_money)
        {
            List<int> estates_to_sell = new List<int>();
            double sum = 0;
            estate_ids.Sort();
            foreach (int estate_id in estate_ids)
            {
                if ((Game.board.GetSquare(estate_id) as MonopolyComponent).GetLevel() == 0)
                {
                    if (sum < need_money)
                    {
                        sum += (Game.board.GetSquare(estate_id) as MonopolyComponent).GetSellPrice();
                        estates_to_sell.Add(estate_id);
                    }
                    else
                    {
                        break;
                    }
                }           
            }
            estate_ids.Sort();
            return estates_to_sell;
        }
        
        private int SellMonopoly(int need_money)
        {
            int got_money = 0;
            while ((estate_ids.Count > 0) && (got_money < need_money))
            {
                int money_for_sale = (Game.board.GetSquare(estate_ids[0]) as MonopolyComponent).GetMonopolyINFO().SellHouse();
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
            estate_ids.Sort();
            foreach (int estate_id in estate_ids)
            {
                Console.ForegroundColor = EstateCell.colors[(Game.board.GetSquare(estate_id) as MonopolyComponent).GetMonopolyKey() - 1];
                Console.WriteLine(Game.board.GetSquare(estate_id));
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
