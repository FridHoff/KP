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
using System.Windows.Shapes;

namespace WpfApplicationEntity.Forms
{
    /// <summary>
    /// Логика взаимодействия для GroupWindow.xaml
    /// </summary>
    public partial class ShopWindow : Window
    {
        private bool add_edit=false;
        int index;
        public ShopWindow()
        {
            InitializeComponent();
        }
        public ShopWindow(int id)
        {
            InitializeComponent();
            this.add_edit = true;
            index = id;
            using (WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext())
            {
                WpfApplicationEntity.API.Shop shop = WpfApplicationEntity.API.DatabaseRequest.GetShopsById(objectMyDBContext, index);
                textBlockAddEditShop.Text = shop.number.ToString();
            }
            ButtonAddEditShop.Content = "Изменить";
            this.Title = "Изменение цеха";
        }

        private void ButtonAddEditShop_Click(object sender, RoutedEventArgs e)
        {            
                if (textBlockAddEditShop.Text != string.Empty)
                {
                    WpfApplicationEntity.API.Shop objectShop = new WpfApplicationEntity.API.Shop();
                    objectShop.number = textBlockAddEditShop.Text;
                    try
                    {
                        using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                         new WpfApplicationEntity.API.MyDBContext())
                        {
                            if (add_edit == false)
                                objectMyDBContext.Shops.Add(objectShop);
                            else
                            {
                                objectShop.ID = index;
                                WpfApplicationEntity.API.Shop objectFromDataBase = new WpfApplicationEntity.API.Shop();
                                objectFromDataBase = WpfApplicationEntity.API.DatabaseRequest.GetShopsById(objectMyDBContext, index);
                                objectMyDBContext.Entry(objectFromDataBase).CurrentValues.SetValues(objectShop);
                            }
                            objectMyDBContext.SaveChanges();
                        }
                        if (add_edit == false)
                            MessageBox.Show("Цех добавлен");
                        else
                            MessageBox.Show("Цех изменён");
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
