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
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private bool add_edit;
        public ProductWindow()
        {
            InitializeComponent();
        }
        public ProductWindow(bool add_edit)
        {
            InitializeComponent();
            this.add_edit = add_edit;
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (this.add_edit == true)
                if (textBlockAddEditname.Text != string.Empty
                    && textBlockAddEditprice.Text != string.Empty
                    && textBlockAddEditlshelf_life.Text != string.Empty
                    && textBlockAddEditltype.Text != string.Empty)
                {
                    WpfApplicationEntity.API.Product objectProduct = new WpfApplicationEntity.API.Product();
                    objectProduct.name = textBlockAddEditname.Text;
                    objectProduct.price = Convert.ToDouble(textBlockAddEditprice.Text);
                    objectProduct.shelf_life = textBlockAddEditlshelf_life.Text;
                    //objectProduct.type = textBlockAddEditlphone.Text;
                    try
                    {
                        using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                            new WpfApplicationEntity.API.MyDBContext())
                        {
                            objectMyDBContext.Products.Add(objectProduct);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Продукт добавлен");
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
