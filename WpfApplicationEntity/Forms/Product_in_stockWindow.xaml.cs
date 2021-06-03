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
    /// Логика взаимодействия для Product_in_stockWindow.xaml
    /// </summary>
    public partial class Product_in_stockWindow : Window
    {
        private bool add_edit;
        public Product_in_stockWindow()
        {
            InitializeComponent();
        }
        public Product_in_stockWindow(bool add_edit)
        {
            InitializeComponent();
            this.add_edit = add_edit;
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (this.add_edit == true)
                if (count.Text != string.Empty
                    && manufacturing_date.Text != string.Empty
                    && shop.Text != string.Empty
                    && product.Text != string.Empty
                    && employee.Text != string.Empty)
                {
                    WpfApplicationEntity.API.Product_in_stock objectProduct_in_stock = new WpfApplicationEntity.API.Product_in_stock();
                    objectProduct_in_stock.count = Convert.ToInt32(count.Text);
                    objectProduct_in_stock.manufacture_date = manufacturing_date.Text;                    
                    try
                    {
                        using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                            new WpfApplicationEntity.API.MyDBContext())
                        {
                            objectMyDBContext.Product_in_stocks.Add(objectProduct_in_stock);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Прадукция на складе добавлена");
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
