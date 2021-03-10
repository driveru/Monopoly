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
        } 
    }
}
