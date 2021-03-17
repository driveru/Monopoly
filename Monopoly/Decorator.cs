 using System;
using System.Collections.Generic;
using System.Text;

namespace Monopoly
{
    abstract class Decorator : MonopolyComponent
    {
        protected MonopolyComponent estateCell;
        public Decorator(MonopolyComponent estateCell)
        {
            this.estateCell = estateCell;
            this.id = estateCell.id;
        }
        public override int GetRent()
        {
            return estateCell.GetRent();
        }
        public override int GetLevel()
        {
            return estateCell.GetLevel();
        }
        public override int GetMonopolyKey()
        {
            return estateCell.GetMonopolyKey();
        }
        public override int GetPrice()
        {
            return estateCell.GetPrice();
        }
        public override int GetSellPrice()
        {
            return estateCell.GetSellPrice();
        }
        public override int GetId()
        {
            return estateCell.GetId();
        }
        public override string GetLabel()
        {
            return estateCell.GetLabel();
        }
        public override MonopolyINFO GetMonopolyINFO()
        {
            return estateCell.GetMonopolyINFO();
        }
        public override Player GetOwner()
        {
            return estateCell.GetOwner();
        }
        public override MonopolyComponent GetComponent()
        {
            return estateCell.GetComponent();
        }
    }
}
