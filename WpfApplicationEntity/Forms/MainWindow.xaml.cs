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

namespace WpfApplicationEntity
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext())
                {
                    if (objectMyDBContext.Database.Exists() == false)
                    {
                        objectMyDBContext.Database.Create();
                        WpfApplicationEntity.API.Employee objectEmployee = new WpfApplicationEntity.API.Employee();
                        objectEmployee.fName = "user fname";
                        objectEmployee.name = "user";
                        objectEmployee.lName = "user";
                        objectEmployee.position = "admin";
                        objectEmployee.login = "user";
                        objectEmployee.password = "1111";
                        objectEmployee.birth_date = "555555";
                        objectEmployee.address = "home";
                        objectEmployee.phone = "543543";
                        objectEmployee.position_set_date = "234556";
                        objectMyDBContext.Employees.Add(objectEmployee);
                        objectMyDBContext.SaveChanges();
                    }
                    //WpfApplicationEntity.API.Employee objectEmployee1 = new WpfApplicationEntity.API.Employee();
                    //objectEmployee1.fName = "user fname";
                    //objectEmployee1.name = "user";
                    //objectEmployee1.lName = "user";
                    //objectEmployee1.position = "admin";
                    //objectEmployee1.login = "user";
                    //objectEmployee1.password = "1111";
                    //objectEmployee1.birth_date = "555555";
                    //objectEmployee1.address = "home";
                    //objectEmployee1.phone = "543543";
                    //objectEmployee1.position_set_date = "234556";
                    //objectMyDBContext.Employees.Add(objectEmployee1);
                    //objectMyDBContext.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.ShowAll();
        }
        #region Цех
        private void addShopButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.ShopWindow g = new Forms.ShopWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        #endregion
        #region Сотрудник
        private void addEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.EmployeeWindow g = new Forms.EmployeeWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }

        #endregion
        #region Заказчики
        private void addCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.CustomerWindow g = new Forms.CustomerWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        #endregion
        #region Продукт
        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.ProductWindow g = new Forms.ProductWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        #endregion
        #region План
        private void addPlanButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.PlanWindow g = new Forms.PlanWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        #endregion
        #region Заказ
        private void addOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.OrderWindow g = new Forms.OrderWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        #endregion
        #region Отгрузка
        private void addShipmentButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.ShipmentWindow g = new Forms.ShipmentWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        #endregion
        #region Продукция на складе
        private void addProduct_in_stockButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.Product_in_stockWindow g = new Forms.Product_in_stockWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        #endregion
        #region Вид продукции
        private void addProduct_typeButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.Product_typeWindow g = new Forms.Product_typeWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        #endregion      
        private void ShowAll()
        {
            try
            {
                using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                    new WpfApplicationEntity.API.MyDBContext())
                {
                    shopGrid.ItemsSource = WpfApplicationEntity.API.DatabaseRequest.GetShops(objectMyDBContext);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





    }
}
