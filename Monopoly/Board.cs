using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace Monopoly
{
    class Board
    {
        private const int boardSize = 40;
        private static IAction[] squares = new IAction[boardSize];
        
        public Board()
        {
            List<MonopolyINFO> monopolies = new List<MonopolyINFO>();
            using (StreamReader sr = new StreamReader("D:/MonopolyInfo.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    MonopolyINFO monopoly_info = (JsonSerializer.Deserialize<MonopolyINFO>(line));
                    monopoly_info.monopoly_estates_ids = new List<int>();
                    monopolies.Add(monopoly_info);
                }
            }
            using (StreamReader sr = new StreamReader("D:/Estates.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    EstateCell estate = (JsonSerializer.Deserialize<EstateCell>(line));
                    squares[estate.id] = new NotMonopoly(estate);
                    foreach (MonopolyINFO mon in monopolies)
                    {
                        if (mon.key == estate.monopoly_key)
                        {
                            mon.monopoly_estates_ids.Add(estate.id);
                            estate.monopolyINFO = mon;
                        }
                    }
                }
            }
            using (StreamReader sr = new StreamReader("D:/TaxesFields.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    TaxesCell tax_field = (JsonSerializer.Deserialize<TaxesCell>(line));
                    squares[tax_field.id] = tax_field;
                }
            }
            using (StreamReader sr = new StreamReader("D:/ChanceFields.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    squares[int.Parse(line)] = new ChanceCell(int.Parse(line));
                }
            }
            squares[30] = new GoToJailCell();
            squares[0] = new StartCell();

            for (int i = 0; i < boardSize; i++)
            {
                if (squares[i] == null)
                    squares[i] = new FreeCell(i);
            }
        }
        public IAction GetSquare(int id)
        {
            return squares[id];
        }
        public IAction GetSquare(IAction current_position, int step)
        {
            return squares[(current_position.id + step) % 40];
        }
        public void BuildField(int id)
        {          
            squares[id] = new Houses(squares[id] as Decorator);
        }
        public void BreakHouse(int id)
        {
            squares[id] = (squares[id] as Decorator).GetComponent();
        }
        public void UpToMonopoly(List<int> ids)
        {
            foreach (int id in ids)
            {
                squares[id] = new UpToMonopoly(squares[id] as Decorator);
            }
        }
        public void DownToEstates(List<int> ids)
        {
            foreach (int id in ids)
            {
                squares[id] = (squares[id] as Decorator).GetComponent();
            }
        }      
    }
}
