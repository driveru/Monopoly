using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Threading;

namespace Monopoly
{
    class Game
    {
        static public Board board;
        static public Player[] players = new Player[2];
        public Game()
        {
            board = new Board();
            for (int i = 0; i < 2; i++)
            {
                Console.Write("Input player name: ");
                players[i] = new Player(Console.ReadLine());
            }         
        }
        public void StartGame()
        {
            int round = 0;
            try
            {
                while (true)
                {
                    Console.Clear();
                    round++;
                    Console.WriteLine($"ROUND {round}");
                    PrintPlayersInventories();
                    foreach (Player player in players)
                    {
                        player.Move();
                        Thread.Sleep(1000);
                        Console.WriteLine();
                    }
                }
            }
            catch (NotEnoughMoneyException e)
            {
                Console.Clear();
                Console.WriteLine($"ROUND {round}");
                Console.WriteLine(e.Message);
                Console.WriteLine($"{e.player.name} lost");
                Console.WriteLine("GAME STATISTIC");
                PrintPlayersInventories();
            }
        }
        private void PrintPlayersInventories()
        {
            foreach (Player player in players)
                player.PrintInfo();
        }
    }
}
