using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Threading;
namespace Monopoly
{
    
    class Program
    {
        static void Main(string[] args)
        {
            
            Game game = new Game();
            game.StartGame();
            
            /*
            Iction[] squares = new IAction[40];

            using (StreamReader sr = new StreamReader("D:/Estates.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    EstateCell estate = (JsonSerializer.Deserialize<EstateCell>(line));
                    squares[estate.id] = new NotMonopoly(estate);
                }
            }
            List<int> ids = new List<int>(new[] { 1, 2 });
            foreach (int id in ids)
            {
                squares[id] = new UpToMonopoly(squares[id] as Decorator);
            }
            var sf = squares[1] as Decorator;
            Houses house = new Houses((squares[1] as MonopolyComponent).GetComponent(), (squares[1] as Decorator).GetLevel() + 1);
            */
        } 
    }
}
