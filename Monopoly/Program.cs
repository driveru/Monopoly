using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;

namespace Monopoly
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            using (StreamWriter sw = new StreamWriter("D:/Data.txt"))
            {
                for (int i = 0; i < 3; i++)
                {
                    string name = Console.ReadLine();
                    int id = int.Parse(Console.ReadLine());
                    int price = int.Parse(Console.ReadLine());
                    EstateField buildField = new EstateField(name, id, price);
                    sw.WriteLine(JsonSerializer.Serialize<GameField>(buildField));                  
                }               
            }
            */

            List<EstateField> estates = new List<EstateField>();
            using (StreamReader sr = new StreamReader("D:/Estates.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null) 
                {
                    estates.Add(JsonSerializer.Deserialize<EstateField>(line));
                }
            }
            foreach (EstateField estate in estates)
            {
                Console.WriteLine(estate);
            }
        } 
    }
}
