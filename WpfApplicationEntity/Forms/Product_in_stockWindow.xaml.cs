using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplicationEntity.API;

namespace WpfApplicationEntity.Forms
{
    /// <summary>
    /// Логика взаимодействия для Product_in_stockWindow.xaml
    /// </summary>
    public partial class Product_in_stockWindow : Window
    {
        private bool add_edit=false;
        int index;
        Employee user;
        public Product_in_stockWindow(Employee user)
        {
            InitializeComponent();
            this.user = user;
        }
        public Product_in_stockWindow(int id, Employee user)
        {
            InitializeComponent();
            this.add_edit = true;
            index = id;
            this.user = user;
            using (WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext())
            {
                WpfApplicationEntity.API.Product_in_stock stock = WpfApplicationEntity.API.DatabaseRequest.GetProductInStockById(objectMyDBContext, index);
               manufacturing_date.Text = stock.manufacture_date;                
                count.Text = stock.count.ToString();
            }
            ButtonAddEdit.Content = "Изменить";        
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                if (count.Text != string.Empty
                    && manufacturing_date.Text != string.Empty
                    && shop.Text != string.Empty
                    && product.Text != string.Empty)
                {
                    WpfApplicationEntity.API.Product_in_stock objectProduct_in_stock = new WpfApplicationEntity.API.Product_in_stock();
                    objectProduct_in_stock.count = Convert.ToInt32(count.Text);
                    objectProduct_in_stock.manufacture_date = manufacturing_date.Text;
                    objectProduct_in_stock.shop = findShop(shop.SelectedItem.ToString());
                    objectProduct_in_stock.product=findProd(product.SelectedItem.ToString());
                    objectProduct_in_stock.employee = user;
                    try
                    {
                        using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                         new WpfApplicationEntity.API.MyDBContext())
                        {
                            if (add_edit == false)
                                objectMyDBContext.Product_in_stocks.Add(objectProduct_in_stock);
                            else
                            {
                                objectProduct_in_stock.ID = index;
                                WpfApplicationEntity.API.Product_in_stock objectFromDataBase = new WpfApplicationEntity.API.Product_in_stock();
                                objectFromDataBase = WpfApplicationEntity.API.DatabaseRequest.GetProductInStockById(objectMyDBContext, index);
                                objectMyDBContext.Entry(objectFromDataBase).CurrentValues.SetValues(objectProduct_in_stock);
                            }
                            objectMyDBContext.SaveChanges();
                        }
                        if (add_edit == false)
                            MessageBox.Show("Продукт добавлен");
                        else
                            MessageBox.Show("Продукт изменён");
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка!");
                    this.DialogResult = false;
                }
            }
            catch
            {
                MessageBox.Show("Не все поля заполнены корректными данными!", "Ошибка!");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (MyDBContext DB = new MyDBContext())
            {
                List<string> numbers = new List<string>();
                var shops = DatabaseRequest.GetShops(DB);
                foreach (var item in shops)
                {
                    numbers.Add(item.ID.ToString());
                }
                shop.ItemsSource = numbers;    
                List<string> pNames = new List<string>();
                var products = DatabaseRequest.GetProduct(DB);
                foreach (var item in products)
                {
                    pNames.Add(item.name.ToString());
                }
                product.ItemsSource = pNames;
            }
        }
        private Shop findShop(string ProdName)
        {
          //  Shop prod = new Shop();
            using (MyDBContext DB = new MyDBContext())
            {
                var customers = DatabaseRequest.GetShops(DB);
                foreach (var item in customers)
                {
                    if (ProdName == item.ID.ToString())
                        return item;
                }
            }
            return null;
        }
        private Product findProd(string ProdName)
        {
           // Product prod = new Product();
            using (MyDBContext DB = new MyDBContext())
            {
                var customers = DatabaseRequest.GetProduct(DB);
                foreach (var item in customers)
                {
                    if (ProdName == item.name.ToString())
                        return item;
                }
            }
            return null;
        }
    }
}
