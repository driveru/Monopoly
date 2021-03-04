using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class EstateField : GameField, IComparable
    {
        public bool IsBought { get; set; }
        public int price { get; set; }
        public Player owner { get; set; }
        public int rent { get; set; }
        public EstateField() { }
        public override string ToString()
        {
            return $"{id}) {label}, price: {price}, rent: {rent}";
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
            EstateField other_estate = obj as EstateField;
            return this.id.CompareTo(other_estate.id);
        }
    }
}
