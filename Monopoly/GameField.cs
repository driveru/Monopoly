﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    abstract class GameField : IAction
    {
        public abstract void Action(Player player);
        public string label { get; set; }
        public int id { get; set; }
    }
}
