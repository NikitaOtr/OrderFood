using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Collections.Generic;

using Menu;
using System;

namespace Order
{
    public partial class MainWindow : Window
    {
        private List<List<Dish>> MenuSections = new List<List<Dish>>();

        private List<Dish> listOfBurgers = new List<Dish>();
        private List<Dish> listOfSnacks = new List<Dish>();
        private List<Dish> listOfBeverages = new List<Dish>();

        private MyList<OrderItem> listOrder = new MyList<OrderItem>();

        private Dish SelectDish = null;
        private int indexOrder = -1;

        public MainWindow()
        {
            InitializeComponent();
            CreateDataBase();
            InitializeDataBase();
        }

        private void CreateDataBase()
        {
            listOfBurgers.Add(new Burger("Чисбургер", 400));
            listOfBurgers.Add(new Burger("Чикенбургер", 370));
            listOfBurgers.Add(new Burger("Фишбургер", 350));
            listOfBurgers.Add(new Burger("Цезарь Бургер", 330));
            listOfBurgers.Add(new Burger("Чёрный Бургер", 320));
            listOfBurgers.Add(new Burger("Шеф Бургер", 310));

            listOfSnacks.Add(new Snack("Картошка фри", 120));
            listOfSnacks.Add(new Snack("Картошка по деревонски", 150));
            listOfSnacks.Add(new Snack("Наггетсы", 200));
            listOfSnacks.Add(new Snack("Cтрипсы", 190));
            listOfSnacks.Add(new Snack("Куриные крылышки", 180));

            listOfBeverages.Add(new Beverage("Капучино", 150));
            listOfBeverages.Add(new Beverage("Латте", 120));
            listOfBeverages.Add(new Beverage("Американо", 130));
            listOfBeverages.Add(new Beverage("Чай чёрный", 90));
            listOfBeverages.Add(new Beverage("Кока-Кола", 80));
            listOfBeverages.Add(new Beverage("Фанта", 70));

            MenuSections.Add(listOfBurgers);
            MenuSections.Add(listOfSnacks);
            MenuSections.Add(listOfBeverages);
        }

        private void InitializeDataBase()
        {
            UIMenuSections.Items.Add("Бургеры");
            UIMenuSections.Items.Add("Закуски");
            UIMenuSections.Items.Add("Напитки");
        }


        private void SetDishes(List<Dish> list)
        {
            UIDishes.Items.Clear();
            foreach(Dish dish in list) {
                UIDishes.Items.Add(dish);
            }
        }

        private void SetPrice(Label label, int price)
        {
            label.Content = price + " руб.";
        }

        private void SetCountSelectedDish(int count)
        {
            UICountSelectDish.Text = count.ToString();
        }


        private void onChangeMenuSection(object sender, RoutedEventArgs e)
        {
            UIOrder.SelectedIndex = -1;
            if (indexOrder == -1)
            {
                onClickClearSelectDish(new Object(), new RoutedEventArgs());
            }

            SetDishes(MenuSections[UIMenuSections.SelectedIndex]);
        }

        private void onChangeDishes(object sender, RoutedEventArgs e)
        {
            if (UIDishes.SelectedIndex < 0)
            {
                return;
            }
            UIOrder.SelectedIndex = -1;
            indexOrder = -1;

            SelectDish = MenuSections[UIMenuSections.SelectedIndex][UIDishes.SelectedIndex];
            UISelectDish.Content = SelectDish.Name;
            SetPrice(UIPriceSelectDish, SelectDish.Price);
            SetPrice(UITotalPriceSelectDish, SelectDish.Price);
            SetCountSelectedDish(1);

            ButtonCleanSelectionDish.IsEnabled = true;
            ButtonChangeOrderDish.IsEnabled = false;
            ButtonAddToOrder.IsEnabled = true;
        }

        private void onChangeOrder(object sender, RoutedEventArgs e)
        {
            if (UIOrder.SelectedIndex <  0)
            {
                return;
            }
            UIDishes.SelectedIndex = -1;


            indexOrder = UIOrder.SelectedIndex;
            OrderItem orderItem = listOrder[indexOrder];
            SelectDish = orderItem.Dish;
            UISelectDish.Content = SelectDish.Name;
            SetCountSelectedDish(orderItem.Count);
            SetPrice(UIPriceSelectDish, orderItem.Price);
            SetPrice(UITotalPriceSelectDish, orderItem.TotalPrice);

            ButtonCleanSelectionDish.IsEnabled = true;
            ButtonChangeOrderDish.IsEnabled = true;
            ButtonAddToOrder.IsEnabled = false;
        }
        

        private void onClickMinusCount(object sender, RoutedEventArgs e)
        {
            try
            {
                int count = Int32.Parse(UICountSelectDish.Text);
                int price = SelectDish.Price;
                if (count > 1)
                {
                    count -= 1;
                    SetPrice(UITotalPriceSelectDish, price * count);
                    SetCountSelectedDish(count);
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Необходимо выбрать товар");
            }
        }

        private void onClickPlusCount(object sender, RoutedEventArgs e)
        {
            try
            {
                int count = Int32.Parse(UICountSelectDish.Text);
                int price = SelectDish.Price;
                if (count < 99)
                {
                    count += 1;
                    SetPrice(UITotalPriceSelectDish, price * count);
                    SetCountSelectedDish(count);
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Необходимо выбрать товар");
            }
        }

        private void onClickAddToOrder(object sender, RoutedEventArgs e)
        {
            int count = Int32.Parse(UICountSelectDish.Text);
            OrderItem orderItem = new OrderItem(SelectDish, count);
            listOrder.Add(orderItem);
            UIOrder.Items.Add(orderItem.PrintToOrder());
            SetPrice(UISumPriceOrder, listOrder.CalculateTotalSum());
        }

        private void onClickChangeInOrder(object sender, RoutedEventArgs e)
        {
            listOrder[indexOrder].Count = Int32.Parse(UICountSelectDish.Text);
            UIOrder.Items[indexOrder] = listOrder[indexOrder].PrintToOrder();
            SetPrice(UISumPriceOrder, listOrder.CalculateTotalSum());
        }

        private void onClickClearSelectDish(object sender, RoutedEventArgs e)
        {
            ButtonCleanSelectionDish.IsEnabled = false;
            ButtonAddToOrder.IsEnabled = false;
            ButtonChangeOrderDish.IsEnabled = false;
            SelectDish = null;
            UISelectDish.Content = "";
            SetCountSelectedDish(0);
            SetPrice(UIPriceSelectDish, 0);
            SetPrice(UITotalPriceSelectDish, 0);
            indexOrder = -1;
            
        }

        private void onClickDeleteFromOrder(object sender, RoutedEventArgs e)
        {
            if (listOrder.Count == 0)
            {
                MessageBox.Show("Заказ уже пуст!!!");
                return;
            }

            int size = listOrder.Count;
            if (indexOrder < 0)
            {
                listOrder.RemoveAt(size - 1);
                UIOrder.Items.RemoveAt(size - 1);
            } 
            else
            {
                listOrder.RemoveAt(indexOrder);
                UIOrder.Items.RemoveAt(indexOrder);
                onClickClearSelectDish(new Object(), new RoutedEventArgs());
                indexOrder = -1;
            }
            SetPrice(UISumPriceOrder, listOrder.CalculateTotalSum());
        }

        private void onClickClearOrder(object sender, RoutedEventArgs e)
        {
            if (listOrder.Count == 0)
            {
                MessageBox.Show("Заказ уже пуст!!!");
                return;
            }

            listOrder.Clear();
            UIOrder.Items.Clear();
            SetPrice(UISumPriceOrder, 0);
            if (indexOrder > - 1)
            {
                onClickClearSelectDish(new Object(), new RoutedEventArgs());
                indexOrder = -1;
            }
        }


        private void SaveToFile(object sender, RoutedEventArgs e)
        {
            if (listOrder.Count == 0 )
            {
                MessageBox.Show("Для сохранения необходимо, чтобы заказ имел хотябы одну позицию");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "MyOrder";
            saveFileDialog.DefaultExt = ".text";
            saveFileDialog.Filter = "Text documents|*.txt";

            bool isSuccessfullySelectedFile = (bool) saveFileDialog.ShowDialog();
            if (isSuccessfullySelectedFile)
            {
                string path = saveFileDialog.FileName;
                using (StreamWriter streamWriter = new StreamWriter(path, false))
                {
                   foreach(OrderItem orderItem in listOrder)
                   {
                      streamWriter.WriteLine(orderItem.PrintToOrder());
                   }
                   streamWriter.WriteLine("");
                   streamWriter.WriteLine("Сумма заказа: " + listOrder.CalculateTotalSum() + " руб.");
                }
                MessageBox.Show("Заказ успешно сохранён");
            }
        }

    }
}
