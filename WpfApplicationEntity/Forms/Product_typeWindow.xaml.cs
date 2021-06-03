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
    /// Логика взаимодействия для Product_typeWindow.xaml
    /// </summary>
    public partial class Product_typeWindow : Window
    {
        private bool add_edit;
        public Product_typeWindow()
        {
            InitializeComponent();
        }
        public Product_typeWindow(bool add_edit)
        {
            InitializeComponent();
            this.add_edit = add_edit;
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (this.add_edit == true)
                if (name.Text != string.Empty
                    && shop.Text != string.Empty)
                {
                    WpfApplicationEntity.API.Product_Type objectProduct_type = new WpfApplicationEntity.API.Product_Type();
                    objectProduct_type.name = name.Text;                   
                    try
                    {
                        using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                            new WpfApplicationEntity.API.MyDBContext())
                        {
                            objectMyDBContext.Product_types.Add(objectProduct_type);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Вид продукции добавлен");
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
