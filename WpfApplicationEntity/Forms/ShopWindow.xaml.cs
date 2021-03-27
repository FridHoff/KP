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
        private bool add_edit;
        public ShopWindow()
        {
            InitializeComponent();
        }
        public ShopWindow(bool add_edit)
        {
            InitializeComponent();
            this.add_edit = add_edit;
        }

        private void ButtonAddEditShop_Click(object sender, RoutedEventArgs e)
        {
            if (this.add_edit == true)
                if (textBlockAddEditShop.Text != string.Empty)
                {
                    WpfApplicationEntity.API.Shop objectShop = new WpfApplicationEntity.API.Shop();
                    objectShop.number = Convert.ToInt32(textBlockAddEditShop.Text);
                    try
                    {
                        using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                            new WpfApplicationEntity.API.MyDBContext())
                        {
                            objectMyDBContext.Shops.Add(objectShop);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Цех добавлен");
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
