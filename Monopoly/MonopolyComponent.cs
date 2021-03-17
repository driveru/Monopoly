using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    abstract class MonopolyComponent : IAction
    {
        public MonopolyINFO monopolyINFO { get; set; }
        protected bool IsBought { get; set; }
        public int price { get; set; }
        protected Player owner { get; set; }
        public int monopoly_key { get; set; }
        public string label { get; set; }
        public int id { get; set; }
        public abstract int GetRent();
        public abstract int GetLevel();
        public abstract int Sell();
        public abstract int GetSellPrice();
        public abstract void Action(Player player);
        public abstract int GetMonopolyKey();
        public abstract int GetPrice();
        public abstract int GetId();
        public abstract string GetLabel();
        public abstract MonopolyINFO GetMonopolyINFO();
        public abstract Player GetOwner();
        public abstract MonopolyComponent GetComponent();
    }
}
