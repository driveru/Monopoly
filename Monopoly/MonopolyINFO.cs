using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class MonopolyINFO
    {
        //public ConsoleColor color { get; set; }
        public int house_price { get; set; }
        public int key { get; set; }
        public double rent_multiplier { get; set; }
        public List<EstateField> monopoly_estates;
        public void UpToMonopoly()
        {
            foreach (EstateField estate in this.monopoly_estates)
                estate.UpEstateLevel();
        }
        public void BuildField()
        {
            monopoly_estates.Sort(new MonopolyLevelComparer());
            if (monopoly_estates[0].monopoly_level < 6)
                monopoly_estates[0].UpEstateLevel();
        }
        public int SellHouse()
        {
            int got_money = 0;
            monopoly_estates.Sort(new MonopolyLevelComparer());
            monopoly_estates.Reverse();
            if (monopoly_estates[0].monopoly_level == 1)
            {
                foreach (EstateField estate in monopoly_estates)
                {
                    estate.DownEstateLevel();
                }
            }
            else
            {
                monopoly_estates[0].DownEstateLevel();
                got_money += house_price;
            }
            return got_money;
        }
    }
}
