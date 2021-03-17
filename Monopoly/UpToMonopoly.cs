using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class UpToMonopoly : Decorator
    {
        public UpToMonopoly(MonopolyComponent estateCell) : base(estateCell) { }
        public override int GetRent()
        {
            return estateCell.GetRent() * 2;
        }
        public override int GetLevel()
        {
            return 1;
        }

        public override void Action(Player player)
        {
            player.PayRent(GetOwner(), GetRent());
        }

        public override int Sell()
        {
            return estateCell.Sell();
        }

        public override int GetSellPrice()
        {
            return estateCell.GetSellPrice();
        }
        public override string ToString()
        {
            return $"{GetId()}) {GetLabel()}, price: {GetPrice()}, rent: {GetRent()}, level: {GetLevel()}";
        }
        public override MonopolyComponent GetComponent()
        {
            return estateCell.GetComponent();
        }
    }
}
