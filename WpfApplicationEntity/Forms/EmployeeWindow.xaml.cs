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
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        private bool add_edit;
        public EmployeeWindow()
        {
            InitializeComponent();
        }
        public EmployeeWindow(bool add_edit)
        {
            InitializeComponent();
            this.add_edit = add_edit;
        }

        private void ButtonAddEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (this.add_edit == true)                                                    
                if (textBlockAddEditaddress.Text != string.Empty
                    && textBlockAddEditbirth_date.Text != string.Empty
                    && textBlockAddEditfname.Text != string.Empty
                    && textBlockAddEditlName.Text != string.Empty
                    && textBlockAddEditlogin.Text != string.Empty
                    && textBlockAddEditname.Text != string.Empty
                    && textBlockAddEditpassword.Text != string.Empty
                    && textBlockAddEditphone.Text != string.Empty
                    && textBlockAddEditposition.Text != string.Empty
                    && textBlockAddEditposition_set_date.Text != string.Empty
                    && textBlockAddEditshop.Text != string.Empty)
                {
                    WpfApplicationEntity.API.Employee objectEmployee = new WpfApplicationEntity.API.Employee();
                    objectEmployee.address = textBlockAddEditaddress.Text;
                    objectEmployee.birth_date =textBlockAddEditbirth_date.Text;
                    objectEmployee.fName =textBlockAddEditfname.Text;
                    objectEmployee.lName =textBlockAddEditlName.Text;
                    objectEmployee.login =textBlockAddEditlogin.Text;
                    objectEmployee.name =textBlockAddEditname.Text;
                    objectEmployee.password =textBlockAddEditpassword.Text;
                    objectEmployee.phone =textBlockAddEditphone.Text;
                    objectEmployee.position =textBlockAddEditposition.Text;
                    objectEmployee.position_set_date =textBlockAddEditposition_set_date.Text;
                   // objectEmployee.shop = (ICollection<API.Shop>)textBlockAddEditshop.Text;
                    try
                    {
                        using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                            new WpfApplicationEntity.API.MyDBContext())
                        {
                            objectMyDBContext.Employees.Add(objectEmployee);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Сотрудник добавлен");
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
