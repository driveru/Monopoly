using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class EstateCell : MonopolyComponent, IComparable
    {
        static public ConsoleColor[] colors = { ConsoleColor.DarkYellow, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Red, ConsoleColor.DarkRed, 
                                                ConsoleColor.DarkCyan, ConsoleColor.DarkGreen, ConsoleColor.DarkBlue, ConsoleColor.DarkMagenta };
        public int _baseRent { get; set; }
        public EstateCell() { }
        public override string ToString()
        {
            return $"{GetId()}) {GetLabel()}, price: {GetPrice()}, rent: {GetRent()}, level: {GetLevel()}";
        }
        public override void Action(Player player)
        {
            if (IsBought)
            {
                if (player != owner)
                {
                    player.PayRent(owner, GetRent());
                }
                else
                {
                    Console.WriteLine($"{player.name} got on your own field!");
                }
            }
            else
                player.Buy(this);
        }
        public int CompareTo(object obj)
        {
            EstateCell other_estate = obj as EstateCell;
            return this.id.CompareTo(other_estate.id);
        }
        public bool Buy(Player player, int player_money)
        {
            bool successful = false;
            if (player_money >= this.price)
            {
                owner = player;
                successful = true;
                this.IsBought = true;
            }
            return successful;
        }
        public override int Sell()
        {
            this.IsBought = false;
            this.owner = null;
            return GetSellPrice();
        }
        public override int GetSellPrice()
        {
            return (int)Math.Round(this.GetPrice() * 0.7);
        }
        public override int GetRent()
        {
            return _baseRent;
        }
        public override int GetLevel()
        {
            return 0;
        }
        public override int GetMonopolyKey()
        {
            return monopoly_key;
        }
        public override int GetPrice()
        {
            return price;
        }
        public override int GetId()
        {
            return id;
        }
        public override string GetLabel()
        {
            return label;
        }
        public override MonopolyINFO GetMonopolyINFO()
        {
            return monopolyINFO;
        }
        public override Player GetOwner()
        {
            return owner;
        }
        public override MonopolyComponent GetComponent()
        {
            return this;
        }
    }
}
