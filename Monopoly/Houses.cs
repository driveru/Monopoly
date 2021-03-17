using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    class Houses : Decorator
    {

        public Houses(MonopolyComponent estateCell) : base(estateCell)  { }
        public override int GetRent()
        {
            return (int)(estateCell.GetRent() * GetMonopolyINFO().rent_multiplier);
        }
        public override int GetLevel()
        {
            return estateCell.GetLevel() + 1;
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
        public override MonopolyComponent GetComponent()
        {
            return estateCell;
        }
        public override string ToString()
        {
            return $"{GetId()}) {GetLabel()}, price: {GetPrice()}, rent: {GetRent()}, level: {GetLevel()}";
        }
    }
}
