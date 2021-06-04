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
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Threading;

namespace WpfApplicationEntity.Forms
{
    /// <summary>
    /// Логика взаимодействия для StorekeeperWindow.xaml
    /// </summary>
    public partial class StorekeeperWindow : System.Windows.Window
    {
        Employee currentUser;
        public StorekeeperWindow()
        {
            InitializeComponent();
        }
        public StorekeeperWindow(Employee user)
        {
            InitializeComponent();
            this.currentUser = user;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ShowAll();
        }
        private void ShowAll()
        {
            try
            {
                using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                    new WpfApplicationEntity.API.MyDBContext())
                {                      
                    
                    customersGrid.ItemsSource = WpfApplicationEntity.API.DatabaseRequest.GetCustomer(objectMyDBContext);
                    ordersGrid.ItemsSource = WpfApplicationEntity.API.DatabaseRequest.GetOrders(objectMyDBContext);
                    shipmentsGrid.ItemsSource = WpfApplicationEntity.API.DatabaseRequest.GetShipment(objectMyDBContext);
                    product_in_stockGrid.ItemsSource = WpfApplicationEntity.API.DatabaseRequest.GetProductInStock(objectMyDBContext);                                                            
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #region Отгрузка
        private void addShipmentButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.ShipmentWindow g = new Forms.ShipmentWindow();
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        private void deleteShipmentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (shipmentsGrid.SelectedItem != null)
                {
                    WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext();
                    var del = (Shipment)shipmentsGrid.SelectedItem;
                    var list = (from item in objectMyDBContext.Shipments.ToList()
                                where item.ID.CompareTo(del.ID) == 0
                                select item).ToList();
                    objectMyDBContext.Shipments.Remove(list[0]);
                    objectMyDBContext.SaveChanges();
                    this.ShowAll();
                }
                else
                {
                    MessageBox.Show("Не выбрано поле для удаления", "Ошибка");
                }
            }
            catch
            {
                MessageBox.Show("Не удалось удалить запись, так как она связана с другими", "Ошибка");
            }
        }
        private void editShipmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (shipmentsGrid.SelectedItem != null)
            {
                var del = (Shipment)shipmentsGrid.SelectedItem;
                Forms.ShipmentWindow g = new Forms.ShipmentWindow(del.ID);
                if (g.ShowDialog() == true)
                    this.ShowAll();
            }
            else
            {
                MessageBox.Show("Не выбрано поле для изменения", "Ошибка");
            }
        }
        #endregion
        #region Заказчики
        private void addCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.CustomerWindow g = new Forms.CustomerWindow();
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        private void deleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (customersGrid.SelectedItem != null)
                {
                    WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext();
                    var del = (Customer)customersGrid.SelectedItem;
                    var list = (from item in objectMyDBContext.Customers.ToList()
                                where item.ID.CompareTo(del.ID) == 0
                                select item).ToList();
                    objectMyDBContext.Customers.Remove(list[0]);
                    objectMyDBContext.SaveChanges();
                    this.ShowAll();
                }
                else
                {
                    MessageBox.Show("Не выбрано поле для удаления", "Ошибка");
                }
            }
            catch
            {
                MessageBox.Show("Не удалось удалить запись, так как она связана с другими", "Ошибка");
            }
        }
        private void editCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            if (customersGrid.SelectedItem != null)
            {
                var del = (Customer)customersGrid.SelectedItem;
                Forms.CustomerWindow g = new Forms.CustomerWindow(del.ID);
                if (g.ShowDialog() == true)
                    this.ShowAll();
            }
            else
            {
                MessageBox.Show("Не выбрано поле для изменения", "Ошибка");
            }
        }
        private void ReportCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "TimesNewRoman";
            workSheet.Cells[1, 1].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 1].Interior.ColorIndex = 17;
            workSheet.Cells[1, 1] = "Наименование";
            workSheet.Cells[1, 2].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 2].Interior.ColorIndex = 17;
            workSheet.Cells[1, 2] = "Адресс";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "Телефон";
            int i = 2;
            try
            {
                using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                        new WpfApplicationEntity.API.MyDBContext())
                {
                    List<Customer> customers = WpfApplicationEntity.API.DatabaseRequest.GetCustomer(objectMyDBContext).ToList();
                    foreach (Customer customer in customers)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = customer.Name;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 2] = customer.address;
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = customer.phone;
                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory +
                        "\\Заказчики.xls";
                    workSheet.SaveAs(pathToXlsFile);
                    exApp.Quit();
                    MessageBox.Show("Отчёт создан!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
        #region Заказ
        private void addOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.OrderWindow g = new Forms.OrderWindow(currentUser);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        private void deleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ordersGrid.SelectedItem != null)
                {
                    WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext();
                    var del = (Order)ordersGrid.SelectedItem;
                    var list = (from item in objectMyDBContext.Orders.ToList()
                                where item.ID.CompareTo(del.ID) == 0
                                select item).ToList();
                    objectMyDBContext.Orders.Remove(list[0]);
                    objectMyDBContext.SaveChanges();
                    this.ShowAll();
                }
                else
                {
                    MessageBox.Show("Не выбрано поле для удаления", "Ошибка");
                }
            }
            catch
            {
                MessageBox.Show("Не удалось удалить запись, так как она связана с другими", "Ошибка");
            }
        }
        private void editOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (ordersGrid.SelectedItem != null)
            {
                var del = (Order)ordersGrid.SelectedItem;
                Forms.OrderWindow g = new Forms.OrderWindow(del.ID, currentUser);
                if (g.ShowDialog() == true)
                    this.ShowAll();
            }
            else
            {
                MessageBox.Show("Не выбрано поле для изменения", "Ошибка");
            }
        }
        private void ReportOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "TimesNewRoman";
            workSheet.Cells[1, 1].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 1].Interior.ColorIndex = 17;
            workSheet.Cells[1, 1] = "Дата";
            workSheet.Cells[1, 2].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 2].Interior.ColorIndex = 17;
            workSheet.Cells[1, 2] = "Статус";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "Количество";
            workSheet.Cells[1, 4].EntireColumn.ColumnWidth = 25;
            workSheet.Cells[1, 4].Interior.ColorIndex = 17;
            workSheet.Cells[1, 4] = "Заказчик";
            workSheet.Cells[1, 5].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 5].Interior.ColorIndex = 17;
            workSheet.Cells[1, 5] = "Сотрудник";
            workSheet.Cells[1, 6].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 6].Interior.ColorIndex = 17;
            workSheet.Cells[1, 6] = "Продукт";
            int i = 2;
            try
            {
                using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                        new WpfApplicationEntity.API.MyDBContext())
                {
                    List<Order> orders = WpfApplicationEntity.API.DatabaseRequest.GetOrders(objectMyDBContext);
                    foreach (Order order in orders)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = order.date;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        if (order.status)
                            workSheet.Cells[i, 2] = "Готов";
                        else
                            workSheet.Cells[i, 2] = "Не готов";
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = order.count;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 4] = order.customer.Name;
                        workSheet.Cells[i, 5].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 5] = order.employee.fName;
                        workSheet.Cells[i, 6].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 6] = order.product.name;
                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory +
                        "\\Заказы.xls";
                    workSheet.SaveAs(pathToXlsFile);
                    exApp.Quit();
                    MessageBox.Show("Отчёт создан!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
        #region Продукция на складе
        private void addProduct_in_stockButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.Product_in_stockWindow g = new Forms.Product_in_stockWindow(currentUser);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        private void deleteProduct_in_stockButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (product_in_stockGrid.SelectedItem != null)
                {
                    WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext();
                    var del = (Product_in_stock)product_in_stockGrid.SelectedItem;
                    var list = (from item in objectMyDBContext.Product_in_stocks.ToList()
                                where item.ID.CompareTo(del.ID) == 0
                                select item).ToList();
                    objectMyDBContext.Product_in_stocks.Remove(list[0]);
                    objectMyDBContext.SaveChanges();
                    this.ShowAll();
                }
                else
                {
                    MessageBox.Show("Не выбрано поле для удаления", "Ошибка");
                }
            }
            catch
            {
                MessageBox.Show("Не удалось удалить запись, так как она связана с другими", "Ошибка");
            }
        }
        private void editProduct_in_stockButton_Click(object sender, RoutedEventArgs e)
        {
            if (product_in_stockGrid.SelectedItem != null)
            {
                var del = (Product_in_stock)product_in_stockGrid.SelectedItem;
                Forms.Product_in_stockWindow g = new Forms.Product_in_stockWindow(del.ID, currentUser);
                if (g.ShowDialog() == true)
                    this.ShowAll();
            }
            else
            {
                MessageBox.Show("Не выбрано поле для изменения", "Ошибка");
            }
        }
        private void ReportProduct_in_stockButton_Click(object sender, RoutedEventArgs e)
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "TimesNewRoman";
            workSheet.Cells[1, 1].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 1].Interior.ColorIndex = 17;
            workSheet.Cells[1, 1] = "Количество";
            workSheet.Cells[1, 2].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 2].Interior.ColorIndex = 17;
            workSheet.Cells[1, 2] = "Дата изготовления";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "Цех";
            workSheet.Cells[1, 4].EntireColumn.ColumnWidth = 25;
            workSheet.Cells[1, 4].Interior.ColorIndex = 17;
            workSheet.Cells[1, 4] = "Продукт";
            workSheet.Cells[1, 5].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 5].Interior.ColorIndex = 17;
            workSheet.Cells[1, 5] = "Сотрудник";
            int i = 2;
            try
            {
                using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                        new WpfApplicationEntity.API.MyDBContext())
                {
                    List<Product_in_stock> stock = WpfApplicationEntity.API.DatabaseRequest.GetProductInStock(objectMyDBContext).ToList();
                    foreach (Product_in_stock product in stock)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = product.count;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 2] = product.manufacture_date;
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = product.shop.number;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 4] = product.product.name;
                        workSheet.Cells[i, 5].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 5] = product.employee.fName;
                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory +
                        "\\Продукция на складе.xls";
                    workSheet.SaveAs(pathToXlsFile);
                    exApp.Quit();
                    MessageBox.Show("Отчёт создан!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
