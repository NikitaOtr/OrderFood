using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public abstract class Dish
    {
        private string name;
        private int price;

        public Dish(string name, int price)
        {
            this.name = name;
            this.price = price;
        }

        public string Name => this.name;

        public int Price => this.price;

        public override string ToString()
        {
            return this.name;
        }

        abstract public string PrintToOrder();
    }
}
