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

namespace WpfApplicationEntity.Forms
{
    /// <summary>
    /// Логика взаимодействия для CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private bool add_edit=false;
        int index;
        public CustomerWindow()
        {
            InitializeComponent();
        }
        public CustomerWindow(int id)
        {
            InitializeComponent();
            this.add_edit = true;
            index = id;
            using (WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext())
            {
                WpfApplicationEntity.API.Customer customer = WpfApplicationEntity.API.DatabaseRequest.GetCustomerById(objectMyDBContext, index);                        
                textBlockAddEditaddress.Text = customer.address;
                textBlockAddEditname.Text = customer.Name;
                textBlockAddEditlphone.Text = customer.phone;               
            }
            ButtonAddEdit.Content = "Изменить";
            this.Title = "Изменение заказчика";
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {           
                if (textBlockAddEditaddress.Text != string.Empty                    
                    && textBlockAddEditlphone.Text != string.Empty
                    && textBlockAddEditname.Text != string.Empty)      
                {
                    WpfApplicationEntity.API.Customer objectCustomer = new WpfApplicationEntity.API.Customer();
                    objectCustomer.address = textBlockAddEditaddress.Text;
                    objectCustomer.Name = textBlockAddEditname.Text;
                    objectCustomer.phone = textBlockAddEditlphone.Text;                    
                    try
                    {
                        using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                            new WpfApplicationEntity.API.MyDBContext())
                        {
                            if (add_edit == false)
                                objectMyDBContext.Customers.Add(objectCustomer);
                            else
                            {
                                objectCustomer.ID = index;
                                WpfApplicationEntity.API.Customer objectFromDataBase = new WpfApplicationEntity.API.Customer();
                                objectFromDataBase = WpfApplicationEntity.API.DatabaseRequest.GetCustomerById(objectMyDBContext, index);
                                objectMyDBContext.Entry(objectFromDataBase).CurrentValues.SetValues(objectCustomer);
                            }
                            objectMyDBContext.SaveChanges();
                        }
                        if (add_edit == false)
                            MessageBox.Show("Заказчик добавлен");
                        else
                            MessageBox.Show("Заказчик изменён");
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
    }
}
