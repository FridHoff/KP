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
        private bool add_edit;
        public CustomerWindow()
        {
            InitializeComponent();
        }
        public CustomerWindow(bool add_edit)
        {
            InitializeComponent();
            this.add_edit = add_edit;
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (this.add_edit == true)
                if (textBlockAddEditaddress.Text != string.Empty
                    && textBlockAddEditaddress.Text != string.Empty
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
                            objectMyDBContext.Customers.Add(objectCustomer);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Заказчик добавлен");
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
