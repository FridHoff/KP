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
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private bool add_edit=false;
        int index;
        public ProductWindow()
        {
            InitializeComponent();
        }
        public ProductWindow(int id)
        {
            InitializeComponent();
            this.add_edit = true;
            index = id;
            using (WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext())
            {
                WpfApplicationEntity.API.Product product = WpfApplicationEntity.API.DatabaseRequest.GetProductById(objectMyDBContext, index);
                textBlockAddEditlshelf_life.Text = product.shelf_life;
                textBlockAddEditprice.Text = product.price.ToString();
                textBlockAddEditname.Text = product.name;
            }
            ButtonAddEdit.Content = "Изменить";
            this.Title = "Изменение продукта";
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                if (textBlockAddEditname.Text != string.Empty
                    && textBlockAddEditprice.Text != string.Empty
                    && textBlockAddEditlshelf_life.Text != string.Empty)
                {
                    WpfApplicationEntity.API.Product objectProduct = new WpfApplicationEntity.API.Product();
                    objectProduct.name = textBlockAddEditname.Text;
                    objectProduct.price = Convert.ToDouble(textBlockAddEditprice.Text);
                    objectProduct.shelf_life = textBlockAddEditlshelf_life.Text;
                    objectProduct.type = findType(type.SelectedItem.ToString());
                    try
                    {
                        using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                         new WpfApplicationEntity.API.MyDBContext())
                        {
                            if (add_edit == false)
                                objectMyDBContext.Products.Add(objectProduct);
                            else
                            {
                                objectProduct.ID = index;
                                WpfApplicationEntity.API.Product objectFromDataBase = new WpfApplicationEntity.API.Product();
                                objectFromDataBase = WpfApplicationEntity.API.DatabaseRequest.GetProductById(objectMyDBContext, index);
                                objectMyDBContext.Entry(objectFromDataBase).CurrentValues.SetValues(objectProduct);
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
            catch
            {
                MessageBox.Show("Не все поля заполнены корректными данными!", "Ошибка!");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (MyDBContext DB = new MyDBContext())
            {
                List<string> tNames = new List<string>();
                var types = DatabaseRequest.GetType(DB);
                foreach (var item in types)
                {
                    tNames.Add(item.name.ToString());
                }
                type.ItemsSource = tNames;               
            }
        }
        private Product_Type findType(string custName)
        {
            //Product_Type cust = new Product_Type();
            using (MyDBContext DB = new MyDBContext())
            {
                var customers = DatabaseRequest.GetType(DB);
                foreach (var item in customers)
                {
                    if (custName == item.name.ToString())
                        return item;
                }
            }
            return null;
        }
    }
}
