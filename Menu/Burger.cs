
using System.Linq;

namespace Menu
{
    public class Burger : Dish
    {
        public Burger(string name, int price) : base(name, price) {}

        public override string ToString()
        {
            return base.ToString() + " (" + this.Price + " руб." + ")";
        }

        public override string PrintToOrder()
        {
            return "Бургеры: " +  base.ToString() + "  " + this.Price + "руб.";
        }
    }

}
