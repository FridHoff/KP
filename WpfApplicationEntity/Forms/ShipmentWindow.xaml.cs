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
    /// Логика взаимодействия для ShipmentWindow.xaml
    /// </summary>
    public partial class ShipmentWindow : Window
    {
        private bool add_edit=false;
        int index;
        public ShipmentWindow()
        {
            InitializeComponent();
        }
        public ShipmentWindow(int id)
        {   
            InitializeComponent();
            this.add_edit = true;
            index = id;
            using (WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext())
            {
                WpfApplicationEntity.API.Shipment shipment = WpfApplicationEntity.API.DatabaseRequest.GetShipmentById(objectMyDBContext, index);
                departuring_date.Text = shipment.departure_date;
                receiving_date.Text = shipment.receiving_date;
                count.Text = shipment.count.ToString();
            }
            ButtonAddEdit.Content = "Изменить";        
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {            
            try
            { 
                if (receiving_date.Text != string.Empty
                    && departuring_date.Text != string.Empty
                    && count.Text != string.Empty
                    && order.Text != string.Empty)
                {
                    WpfApplicationEntity.API.Shipment objectShipment = new WpfApplicationEntity.API.Shipment();
                    objectShipment.departure_date = departuring_date.Text;
                    objectShipment.receiving_date = receiving_date.Text;
                    objectShipment.count = Convert.ToInt32(count.Text);
                    objectShipment.Order = findOrder(order.SelectedItem.ToString());
                    objectShipment.product_in_stock= findStock(product_in_stock.SelectedItem.ToString());
                    try
                    {
                        using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                         new WpfApplicationEntity.API.MyDBContext())
                        {
                            if (add_edit == false)
                                objectMyDBContext.Shipments.Add(objectShipment);
                            else
                            {
                                objectShipment.ID = index;
                                WpfApplicationEntity.API.Shipment objectFromDataBase = new WpfApplicationEntity.API.Shipment();
                                objectFromDataBase = WpfApplicationEntity.API.DatabaseRequest.GetShipmentById(objectMyDBContext, index);
                                objectMyDBContext.Entry(objectFromDataBase).CurrentValues.SetValues(objectShipment);
                            }
                            objectMyDBContext.SaveChanges();
                        }
                        if (add_edit == false)
                            MessageBox.Show("Отгрузка добавлен");
                        else
                            MessageBox.Show("Отгрузка изменён");
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
                var orders = DatabaseRequest.GetOrders(DB);
                foreach (var item in orders)
                {
                    numbers.Add(item.ID.ToString());
                }
                order.ItemsSource = numbers;
                List<string> numbers1 = new List<string>();
                var orders1 = DatabaseRequest.GetProductInStock(DB);
                foreach (var item in orders1)
                {
                    numbers1.Add(item.ID.ToString());
                }
                product_in_stock.ItemsSource = numbers1;
            }
        }
        private Order findOrder(string ProdName)
        {
            Order prod = new Order();
            using (MyDBContext DB = new MyDBContext())
            {
                var customers = DatabaseRequest.GetOrders(DB);
                foreach (var item in customers)
                {
                    if (ProdName == item.ID.ToString())
                        prod = item;
                }
            }
            return prod;
        }
        private Product_in_stock findStock(string ProdName)
        {
            Product_in_stock prod = new Product_in_stock();
            using (MyDBContext DB = new MyDBContext())
            {
                var customers = DatabaseRequest.GetProductInStock(DB);
                foreach (var item in customers)
                {
                    if (ProdName == item.ID.ToString())
                        prod = item;
                }
            }
            return prod;
        }
    }
}
