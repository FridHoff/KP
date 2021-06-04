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
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        private bool add_edit = false;
        int index;
        public EmployeeWindow()
        {
            InitializeComponent();
        }
        public EmployeeWindow(int id)
        {
            InitializeComponent();
            this.add_edit = true;
            index = id;
            using (WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext())
            {
                WpfApplicationEntity.API.Employee employee = WpfApplicationEntity.API.DatabaseRequest.GetEmployeeById(objectMyDBContext, index);
                //WpfApplicationEntity.MyClasses.DatabaseRequest.GetTransportType(objectMyDBContext, this.Type);                
                textBlockAddEditaddress.Text = employee.address;
                textBlockAddEditbirth_date.Text = employee.birth_date;
                textBlockAddEditfname.Text = employee.fName;
                textBlockAddEditlName.Text = employee.lName;
                textBlockAddEditlogin.Text = employee.login;
                textBlockAddEditname.Text = employee.name;
                textBlockAddEditpassword.Text = employee.password;
                textBlockAddEditphone.Text = employee.phone;
                textBlockAddEditposition.Text = employee.position;
                textBlockAddEditposition_set_date.Text = employee.position_set_date;
            }
            ButtonAddEditEmployee.Content = "Изменить";
        }

        private void ButtonAddEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (textBlockAddEditaddress.Text != string.Empty
                && textBlockAddEditbirth_date.Text != string.Empty
                && textBlockAddEditfname.Text != string.Empty
                && textBlockAddEditlName.Text != string.Empty
                && textBlockAddEditlogin.Text != string.Empty
                && textBlockAddEditname.Text != string.Empty
                && textBlockAddEditpassword.Text != string.Empty
                && textBlockAddEditphone.Text != string.Empty
                && textBlockAddEditposition_set_date.Text != string.Empty
                && textBlockAddEditshop.Text != string.Empty)
            {
                WpfApplicationEntity.API.Employee objectEmployee = new WpfApplicationEntity.API.Employee();

                objectEmployee.address = textBlockAddEditaddress.Text;
                objectEmployee.birth_date = textBlockAddEditbirth_date.Text;
                objectEmployee.fName = textBlockAddEditfname.Text;
                objectEmployee.lName = textBlockAddEditlName.Text;
                objectEmployee.login = textBlockAddEditlogin.Text;
                objectEmployee.name = textBlockAddEditname.Text;
                objectEmployee.password = textBlockAddEditpassword.Text;
                objectEmployee.phone = textBlockAddEditphone.Text;
                objectEmployee.position = textBlockAddEditposition.Text;
                objectEmployee.position_set_date = textBlockAddEditposition_set_date.Text;
                objectEmployee.shop = findShop(textBlockAddEditshop.SelectedItem.ToString());
                try
                {
                    using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                        new WpfApplicationEntity.API.MyDBContext())
                    {
                        if (add_edit == false)
                            objectMyDBContext.Employees.Add(objectEmployee);
                        else
                        {
                            objectEmployee.ID = index;
                            WpfApplicationEntity.API.Employee objectEmployeeFromDataBase = new WpfApplicationEntity.API.Employee();
                            objectEmployeeFromDataBase = WpfApplicationEntity.API.DatabaseRequest.GetEmployeeById(objectMyDBContext, index);
                            objectMyDBContext.Entry(objectEmployeeFromDataBase).CurrentValues.SetValues(objectEmployee);
                        }
                        objectMyDBContext.SaveChanges();
                    }
                    if(add_edit==false)
                    MessageBox.Show("Сотрудник добавлен");
                    else
                        MessageBox.Show("Сотрудник изменён");
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
                    numbers.Add(item.ID.ToString());
                }
                textBlockAddEditshop.ItemsSource = numbers;                
            }
        }
        private Shop findShop(string ProdName)
        {
            //Shop prod = new Shop();
            using (MyDBContext DB = new MyDBContext())
            {
                var customers = DatabaseRequest.GetShops(DB);
                foreach (var item in customers)
                {
                    if (ProdName == item.ID.ToString())
                        return item;
                }
            }
            return null;
        }
    }
}
