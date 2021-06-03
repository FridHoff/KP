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
    /// Логика взаимодействия для ForemanWindow.xaml
    /// </summary>
    public partial class ForemanWindow : System.Windows.Window
    {
        Employee currentUser;
        public ForemanWindow()
        {
            InitializeComponent();
        }
        public ForemanWindow(Employee user)
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
                    employeesGrid.ItemsSource = WpfApplicationEntity.API.DatabaseRequest.GetEmployees(objectMyDBContext);                                                       
                    product_in_stockGrid.ItemsSource = WpfApplicationEntity.API.DatabaseRequest.GetProductInStock(objectMyDBContext);
                    product_typeGrid.ItemsSource = WpfApplicationEntity.API.DatabaseRequest.GetType(objectMyDBContext);
                    plansGrid.ItemsSource = WpfApplicationEntity.API.DatabaseRequest.GetPlan(objectMyDBContext);
                    productsGrid.ItemsSource = WpfApplicationEntity.API.DatabaseRequest.GetProduct(objectMyDBContext);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #region Сотрудник
        private void addEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.EmployeeWindow g = new Forms.EmployeeWindow();
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        private void deleteGroupButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (employeesGrid.SelectedItem != null)
                {
                    WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext();
                    var del = (Employee)employeesGrid.SelectedItem;
                    var list = (from item in objectMyDBContext.Employees.ToList()
                                where item.ID.CompareTo(del.ID) == 0
                                select item).ToList();
                    objectMyDBContext.Employees.Remove(list[0]);
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
        private void editEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (employeesGrid.SelectedItem != null)
            {
                var del = (Employee)employeesGrid.SelectedItem;
                Forms.EmployeeWindow g = new Forms.EmployeeWindow(del.ID);
                if (g.ShowDialog() == true)
                    this.ShowAll();
            }
            else
            {
                MessageBox.Show("Не выбрано поле для изменения", "Ошибка");
            }
        }

        private void ReportEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "TimesNewRoman";
            workSheet.Cells[1, 1].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 1].Interior.ColorIndex = 17;
            workSheet.Cells[1, 1] = "Фамилия";
            workSheet.Cells[1, 2].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 2].Interior.ColorIndex = 17;
            workSheet.Cells[1, 2] = "Имя";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "Отчество";
            workSheet.Cells[1, 4].EntireColumn.ColumnWidth = 25;
            workSheet.Cells[1, 4].Interior.ColorIndex = 17;
            workSheet.Cells[1, 4] = "Должность";
            workSheet.Cells[1, 5].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 5].Interior.ColorIndex = 17;
            workSheet.Cells[1, 5] = "Логин";
            workSheet.Cells[1, 6].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 6].Interior.ColorIndex = 17;
            workSheet.Cells[1, 6] = "Пароль";
            workSheet.Cells[1, 7].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 7].Interior.ColorIndex = 17;
            workSheet.Cells[1, 7] = "Дата рождения";
            workSheet.Cells[1, 8].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 8].Interior.ColorIndex = 17;
            workSheet.Cells[1, 8] = "Адрес";
            workSheet.Cells[1, 9].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 9].Interior.ColorIndex = 17;
            workSheet.Cells[1, 9] = "Телефон";
            workSheet.Cells[1, 10].EntireColumn.ColumnWidth = 28;
            workSheet.Cells[1, 10].Interior.ColorIndex = 17;
            workSheet.Cells[1, 10] = "Дата назначения на должность";
            workSheet.Cells[1, 11].EntireColumn.ColumnWidth = 5;
            workSheet.Cells[1, 11].Interior.ColorIndex = 17;
            workSheet.Cells[1, 11] = "Цех";
            int i = 2;
            try
            {
                using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                        new WpfApplicationEntity.API.MyDBContext())
                {
                    List<Employee> employes = WpfApplicationEntity.API.DatabaseRequest.GetEmployees(objectMyDBContext);
                    foreach (Employee employee in employes)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = employee.fName;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 2] = employee.name;
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = employee.lName;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 4] = employee.position;
                        workSheet.Cells[i, 5].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 5] = employee.login;
                        workSheet.Cells[i, 6].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 6] = employee.password;
                        workSheet.Cells[i, 7].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 7] = employee.birth_date;
                        workSheet.Cells[i, 8].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 8] = employee.address;
                        workSheet.Cells[i, 9].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 9] = employee.phone;
                        workSheet.Cells[i, 10].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 10] = employee.position_set_date;
                        workSheet.Cells[i, 11].Interior.ColorIndex = 24;
                        //= WpfApplicationEntity.API.DatabaseRequest.GetShopNum(objectMyDBContext, employee.shop);     
                        if (employee.shop != null)
                            workSheet.Cells[i, 11] = employee.shop.number;
                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory +
                        "\\Сотрудники.xls";
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
        private void searchEmployee_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                      new WpfApplicationEntity.API.MyDBContext())
            {
                List<Employee> searching = WpfApplicationEntity.API.DatabaseRequest.GetEmployees(objectMyDBContext);
                List<Employee> employees = new List<Employee>();
                foreach (var item in searching)
                {
                    if (item.fName.IndexOf(searchEmployee.Text) != -1)
                        employees.Add(item);
                }
                employeesGrid.ItemsSource = employees;
            }
        }
        #endregion
        #region Продукт
        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.ProductWindow g = new Forms.ProductWindow();
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        private void deleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (productsGrid.SelectedItem != null)
                {
                    WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext();
                    var del = (Product)productsGrid.SelectedItem;
                    var list = (from item in objectMyDBContext.Products.ToList()
                                where item.ID.CompareTo(del.ID) == 0
                                select item).ToList();
                    objectMyDBContext.Products.Remove(list[0]);
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
        private void editProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (productsGrid.SelectedItem != null)
            {
                var del = (Product)productsGrid.SelectedItem;
                Forms.ProductWindow g = new Forms.ProductWindow(del.ID);
                if (g.ShowDialog() == true)
                    this.ShowAll();
            }
            else
            {
                MessageBox.Show("Не выбрано поле для изменения", "Ошибка");
            }
        }
        private void ReportProductButton_Click(object sender, RoutedEventArgs e)
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
            workSheet.Cells[1, 2] = "Цена";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "Срок годности";
            workSheet.Cells[1, 4].EntireColumn.ColumnWidth = 25;
            workSheet.Cells[1, 4].Interior.ColorIndex = 17;
            workSheet.Cells[1, 4] = "Вид продукции";
            int i = 2;
            try
            {
                using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                        new WpfApplicationEntity.API.MyDBContext())
                {
                    List<Product> products = WpfApplicationEntity.API.DatabaseRequest.GetProduct(objectMyDBContext).ToList();
                    foreach (Product product in products)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = product.name;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 2] = product.price;
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = product.shelf_life;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 4] = product.type.name;
                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory +
                        "\\Продукция.xls";
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
        #region План
        private void addPlanButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.PlanWindow g = new Forms.PlanWindow();
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        private void deletePlanButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (plansGrid.SelectedItem != null)
                {
                    WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext();
                    var del = (Production_plan)plansGrid.SelectedItem;
                    var list = (from item in objectMyDBContext.Production_plans.ToList()
                                where item.ID.CompareTo(del.ID) == 0
                                select item).ToList();
                    objectMyDBContext.Production_plans.Remove(list[0]);
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
        private void editPlanButton_Click(object sender, RoutedEventArgs e)
        {
            if (plansGrid.SelectedItem != null)
            {
                var del = (Production_plan)plansGrid.SelectedItem;
                Forms.PlanWindow g = new Forms.PlanWindow(del.ID);
                if (g.ShowDialog() == true)
                    this.ShowAll();
            }
            else
            {
                MessageBox.Show("Не выбрано поле для изменения", "Ошибка");
            }
        }
        private void ReportPlanButton_Click(object sender, RoutedEventArgs e)
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
            workSheet.Cells[1, 2] = "Количество";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "Цех";
            int i = 2;
            try
            {
                using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                        new WpfApplicationEntity.API.MyDBContext())
                {
                    List<Production_plan> plans = WpfApplicationEntity.API.DatabaseRequest.GetPlan(objectMyDBContext).ToList();
                    foreach (Production_plan plan in plans)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = plan.date;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 2] = plan.count;
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = plan.shop.number;
                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory +
                        "\\План производства.xls";
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
