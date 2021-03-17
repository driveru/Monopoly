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
        public List<int> monopoly_estates_ids;
        public void UpToMonopoly()
        {
            Game.board.UpToMonopoly(monopoly_estates_ids);
        }
        
        public void BuildField()
        {
            List<MonopolyComponent> monopoly_squares = new List<MonopolyComponent>();
            foreach (int id in monopoly_estates_ids)
            {
                monopoly_squares.Add(Game.board.GetSquare(id) as MonopolyComponent);
            }
            monopoly_squares.Sort(new MonopolyLevelComparer());
            if (monopoly_squares[0].GetLevel() < 6)
            {
                Game.board.BuildField(monopoly_squares[0].id);
            }
        }
        public int SellHouse()
        {
            int got_money = 0;
            List<MonopolyComponent> monopoly_squares = new List<MonopolyComponent>();
            foreach (int id in monopoly_estates_ids)
            {
                monopoly_squares.Add(Game.board.GetSquare(id) as MonopolyComponent);
            }
            monopoly_squares.Sort(new MonopolyLevelComparer());
            monopoly_squares.Reverse();
            if (monopoly_squares[0].GetLevel() == 1)
            {
                Game.board.DownToEstates(monopoly_estates_ids);
            }
            else
            {
                Game.board.BreakHouse(monopoly_squares[0].id);
                got_money += house_price;
            }
            return got_money;
        }
        
    }
}
