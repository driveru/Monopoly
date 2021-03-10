using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace Monopoly
{
    class Board
    {
        private const int boardSize = 40;
        private IAction[] squares = new IAction[boardSize];
        
        public Board()
        {
            List<MonopolyINFO> monopolies = new List<MonopolyINFO>();
            using (StreamReader sr = new StreamReader("D:/MonopolyInfo.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    MonopolyINFO monopoly_info = (JsonSerializer.Deserialize<MonopolyINFO>(line));
                    monopoly_info.monopoly_estates = new List<EstateCell>();
                    monopolies.Add(monopoly_info);
                }
            }
            using (StreamReader sr = new StreamReader("D:/Estates.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    EstateCell estate = (JsonSerializer.Deserialize<EstateCell>(line));
                    squares[estate.id] = estate;
                    foreach (MonopolyINFO mon in monopolies)
                    {
                        if (mon.key == estate.monopoly_key)
                        {
                            mon.monopoly_estates.Add(estate);
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
            squares[30] = new JailCell();
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
    }
}
