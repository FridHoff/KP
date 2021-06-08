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
    /// Логика взаимодействия для Product_typeWindow.xaml
    /// </summary>
    public partial class Product_typeWindow : Window
    {
        private bool add_edit=false;
        int index;
        public Product_typeWindow()
        {
            InitializeComponent();
        }
        public Product_typeWindow(int id)
        {
            InitializeComponent();
            this.add_edit = true;
            index = id;
            using (WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext())
            {
                WpfApplicationEntity.API.Product_Type productType = WpfApplicationEntity.API.DatabaseRequest.GetTypeById(objectMyDBContext, index);
               name.Text = productType.name;
            }
            ButtonAddEdit.Content = "Изменить";
            this.Title = "Изменение виду продукции";
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {            
                if (name.Text != string.Empty
                    && shop.Text != string.Empty)
                {
                    WpfApplicationEntity.API.Product_Type objectProduct_type = new WpfApplicationEntity.API.Product_Type();
                    objectProduct_type.name = name.Text;
                    objectProduct_type.shop = findShop(shop.SelectedItem.ToString());
                    try
                    {
                        using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                         new WpfApplicationEntity.API.MyDBContext())
                        {
                            if (add_edit == false)
                                objectMyDBContext.Product_types.Add(objectProduct_type);
                            else
                            {
                                objectProduct_type.ID = index;
                                WpfApplicationEntity.API.Product_Type objectFromDataBase = new WpfApplicationEntity.API.Product_Type();
                                objectFromDataBase = WpfApplicationEntity.API.DatabaseRequest.GetTypeById(objectMyDBContext, index);
                                objectMyDBContext.Entry(objectFromDataBase).CurrentValues.SetValues(objectProduct_type);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (MyDBContext DB = new MyDBContext())
            {
                List<string> numbers = new List<string>();
                var shops = DatabaseRequest.GetShops(DB);
                foreach (var item in shops)
                {
                    numbers.Add(item.number.ToString());
                }
                shop.ItemsSource = numbers;               
            }
        }
        private Shop findShop(string ProdName)
        {
         //   Shop prod = new Shop();
            using (MyDBContext DB = new MyDBContext())
            {
                var customers = DatabaseRequest.GetShops(DB);
                foreach (var item in customers)
                {
                    if (ProdName == item.number.ToString())
                        return item;
                }
            }
            return null;
        }
    }
}
