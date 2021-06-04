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
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private bool add_edit=false;
        int index;
        Employee user;
        public OrderWindow(Employee user)
        {
            InitializeComponent();
            this.user = user;
        }
        public OrderWindow(int id, Employee user)
        {
            InitializeComponent();
            this.add_edit = true;
            index = id;
            this.user = user;
            using (WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext())
            {
                WpfApplicationEntity.API.Order order = WpfApplicationEntity.API.DatabaseRequest.GetOrdersById(objectMyDBContext, index);
                date.Text = order.date;
                count.Text = order.count.ToString();                
            }
            ButtonAddEdit.Content = "Изменить";
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {           
            try
            {

                if (date.Text != string.Empty
                    && count.Text != string.Empty
                    && customer.Text != string.Empty
                    && product.Text != string.Empty)
                {
                    WpfApplicationEntity.API.Order objectOrder = new WpfApplicationEntity.API.Order();
                    objectOrder.date = date.Text;
                    objectOrder.status = status.IsEnabled;
                    objectOrder.customer = findCust(customer.SelectedItem.ToString());
                    objectOrder.product = findProd(product.SelectedItem.ToString());
                    objectOrder.count = Convert.ToInt32(count.Text);
                    objectOrder.employee = user;
                    try
                    {
                        using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                          new WpfApplicationEntity.API.MyDBContext())
                        {
                            if (add_edit == false)
                                objectMyDBContext.Orders.Add(objectOrder);
                            else
                            {
                                objectOrder.ID = index;
                                WpfApplicationEntity.API.Order objectFromDataBase = new WpfApplicationEntity.API.Order();
                                objectFromDataBase = WpfApplicationEntity.API.DatabaseRequest.GetOrdersById(objectMyDBContext, index);
                                objectMyDBContext.Entry(objectFromDataBase).CurrentValues.SetValues(objectOrder);
                            }
                            objectMyDBContext.SaveChanges();
                        }
                        if (add_edit == false)
                            MessageBox.Show("Заказ добавлен");
                        else
                            MessageBox.Show("Заказ изменён");
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
                List<string> pNames = new List<string>();
                var products = DatabaseRequest.GetProduct(DB);
                foreach (var item in products)
                {
                    pNames.Add(item.name.ToString());
                }
                product.ItemsSource = pNames;
                var customers = DatabaseRequest.GetCustomer(DB);
                List<string> cNames = new List<string>();
                foreach (var item in customers)
                {
                    cNames.Add(item.Name.ToString());
                }
                customer.ItemsSource = cNames;
            }
        }
        private Customer findCust(string custName)
        {
            //Customer cust=new Customer();
            using (MyDBContext DB = new MyDBContext())
            {               
                var customers = DatabaseRequest.GetCustomer(DB);
                foreach (var item in customers)
                {
                    if (custName == item.Name.ToString())
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
