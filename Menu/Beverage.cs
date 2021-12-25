using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public class Beverage : Dish
    {
        public Beverage(string name, int price) : base(name, price) {}

        public override string ToString()
        {
            return base.ToString() + " (" + this.Price + " руб." + ")";
        }

        public override string PrintToOrder()
        {
            return "Напитки: " + base.ToString() + "  " + this.Price + "руб.";
        }
    }
}
