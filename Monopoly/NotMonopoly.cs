using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class NotMonopoly : Decorator
    {
        public NotMonopoly(MonopolyComponent estateCell) : base(estateCell) { }
        public override int GetRent()
        {
            return estateCell.GetRent();
        }
        public override int GetLevel()
        {
            return estateCell.GetLevel();
        }

        public override void Action(Player player)
        {
            estateCell.Action(player);
        }

        public override int Sell()
        {
            return estateCell.Sell();
        }

        public override int GetSellPrice()
        {
            return estateCell.GetSellPrice();
        }
        public override int GetPrice()
        {
            return estateCell.GetPrice();
        }
        public override string ToString()
        {
            return $"{GetId()}) {GetLabel()}, price: {GetPrice()}, rent: {GetRent()}, level: {GetLevel()}";
        }
    }
}
