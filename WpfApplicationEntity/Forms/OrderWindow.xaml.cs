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
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private bool add_edit;
        public OrderWindow()
        {
            InitializeComponent();
        }
        public OrderWindow(bool add_edit)
        {
            InitializeComponent();
            this.add_edit = add_edit;
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (this.add_edit == true)
                if (date.Text != string.Empty
                    && employee.Text != string.Empty
                    && customer.Text != string.Empty)
                {
                    WpfApplicationEntity.API.Order objectOrder = new WpfApplicationEntity.API.Order();
                    objectOrder.date = date.Text;
                    objectOrder.status = status.IsEnabled;                   
                    try
                    {
                        using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                            new WpfApplicationEntity.API.MyDBContext())
                        {
                            objectMyDBContext.Orders.Add(objectOrder);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Заказ добавлен");
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
