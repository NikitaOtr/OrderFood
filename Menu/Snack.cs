using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public class Snack : Dish
    {
        public Snack(string name, int price) : base(name, price){}

        public override string ToString()
        {
            return base.ToString() + " (" + this.Price + " руб." + ")";
        }

        public override string PrintToOrder()
        {
            return "Закуски: " +  base.ToString() + "  " + this.Price + "руб.";
        }
    }
}
