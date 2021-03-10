using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class EstateCell : Square, IComparable
    {
        static public ConsoleColor[] colors = { ConsoleColor.DarkYellow, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Red, ConsoleColor.DarkRed, 
                                                ConsoleColor.DarkCyan, ConsoleColor.DarkGreen, ConsoleColor.DarkBlue, ConsoleColor.DarkMagenta };
        private int _baseRent; 
        public bool IsBought { get; set; }
        public int price { get; set; }
        public Player owner { get; set; }
        public MonopolyINFO monopolyINFO;
        public int rent 
        { 
            get 
            {
                return (int)(_baseRent * Math.Pow(monopolyINFO.rent_multiplier, monopoly_level));
            }
            set { _baseRent = value; } 
        }
        public int monopoly_key { get; set; }
        public int monopoly_level { get; set; }
        public EstateCell() { }
        public override string ToString()
        {
            return $"{id}) {label}, price: {price}, rent: {rent}, level: {monopoly_level}";
        }
        public override void Action(Player player)
        {
            if (IsBought)
            {
                if (player != owner)
                {
                    player.PayRent(owner, rent);
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
        public void UpEstateLevel()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{owner.name} builds {this.label}, rent grows UP !");
            Console.ResetColor();
            monopoly_level++;
        }
        public void DownEstateLevel()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{owner.name} breaks down {this.label}, rent reduced !");
            Console.ResetColor();
            monopoly_level--;
        }
    }
}
