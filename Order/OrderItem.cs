using Menu;

namespace Order
{
    class OrderItem
    {
        private Dish dish;
        private int count;

        public OrderItem(Dish dish, int count)
        {
            this.dish = dish;
            this.count = count;
        }

        public Dish Dish => this.dish;

        public int Price => dish.Price;

        public int TotalPrice => this.count * dish.Price;

        public int Count
        {
            get => this.count;
            set => this.count = value;
        }

        public string PrintToOrder()
        {
            return dish.PrintToOrder() + "      x" + this.count + "      =>      " + this.count * dish.Price + "  рублей";
        }
    }
}
