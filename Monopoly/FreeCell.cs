using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class FreeCell : IAction
    {
        public string label { get; set; }
        public int id { get; set ; }
        public FreeCell(int id)
        {
            this.id = id;
            this.label = "Free Cell";
        }
        public void Action(Player player)
        {
            Console.WriteLine("Nothing happened");
        }
        public string GetLabel()
        {
            return label;
        }
    }
}
