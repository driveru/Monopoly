using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class Die
    {
        public int faceValue { get; private set; }
        public const int Max = 6;
        public Die() 
        {
            roll();
        }
        public void roll()
        {
            Random rnd = new Random();
            faceValue = rnd.Next(1, Max + 1);
        }
        public override string ToString()
        {
            return faceValue.ToString();
        }
    }
}
