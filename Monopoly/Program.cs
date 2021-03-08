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
            List<MonopolyINFO> monopolies = new List<MonopolyINFO>();
            using (StreamReader sr = new StreamReader("D:/MonopolyInfo.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    MonopolyINFO monopoly_info = (JsonSerializer.Deserialize<MonopolyINFO>(line));
                    monopoly_info.monopoly_estates = new List<EstateField>();
                    monopolies.Add(monopoly_info);
                }
            }
            using (StreamReader sr = new StreamReader("D:/Estates.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    EstateField estate = (JsonSerializer.Deserialize<EstateField>(line));
                    map[estate.id] = estate;
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
            map[30] = new JailField();

            Player player_1 = new Player("Bot John");
            Player player_2 = new Player("Bot Gabe");
            int round = 0;

            try
            {
                while (true)
                {
                    Console.Clear();
                    round++;
                    Console.WriteLine($"ROUND {round}");
                    player_1.PrintInfo();
                    player_2.PrintInfo();
                    player_1.Move();
                    Thread.Sleep(100);
                    Console.WriteLine();
                    player_2.Move();
                    Thread.Sleep(100);
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
