using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Threading;
namespace Monopoly
{
    
    class Program
    {
        static public IAction[] map = new IAction[40];
        static void Main(string[] args)
        {
           
            using (StreamReader sr = new StreamReader("D:/Estates.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    EstateField estate = (JsonSerializer.Deserialize<EstateField>(line));
                    map[estate.id] = estate;
                }
            }
            using (StreamReader sr = new StreamReader("D:/TaxesFields.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    TaxesFiled tax_field = (JsonSerializer.Deserialize<TaxesFiled>(line));
                    map[tax_field.id] = tax_field;
                }
            }
            using (StreamReader sr = new StreamReader("D:/ChanceFields.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    map[int.Parse(line)] = new ChanceField(int.Parse(line));
                }
            }

            Player player_1 = new Player("Bot John");
            Player player_2 = new Player("Bot Gabe");
            try
            {
                while (true)
                {
                    Console.Clear();
                    player_1.PrintInfo();
                    player_2.PrintInfo();
                    player_1.Move();
                    Thread.Sleep(1000);
                    Console.WriteLine();
                    player_2.Move();
                    Thread.Sleep(1000);
                }
            }
            catch (NotEnoughMoneyException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.WriteLine($"{e.player.name} lost");
                Console.WriteLine("GAME STATISTIC");
                player_1.PrintInfo();
                player_2.PrintInfo();
            }
        } 
    }
}
