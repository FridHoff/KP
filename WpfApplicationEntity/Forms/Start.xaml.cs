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
    /// Логика взаимодействия для Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext())
            {
                bool flag=true;
                foreach(WpfApplicationEntity.API.Employee employee in objectMyDBContext.Employees.ToList())
                {
                if (login.Text == employee.login && pass.Password==employee.password)
                {
                        if(employee.position=="Администратор")
                        {
                    MainWindow main = new MainWindow(employee);
                    this.Hide();
                    main.Show();                                                
                        }
                        if (employee.position == "Начальник цеха")
                        {
                            ForemanWindow main = new ForemanWindow(employee);
                            this.Hide();
                            main.Show();
                        }
                        if (employee.position == "Кладовщик")
                        {
                            StorekeeperWindow main = new StorekeeperWindow(employee);
                            this.Hide();
                            main.Show();                            
                        }
                        flag = false;
                    break;
                }
                }
                if(flag)
                {
                    MessageBox.Show("Неврно указан логин или пароль");
                    pass.Password = "";
                }
            }
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
                        WpfApplicationEntity.API.Shop objectShop = new WpfApplicationEntity.API.Shop();
                        objectShop.number = "Нет";                        
                        objectMyDBContext.Shops.Add(objectShop);                        
                        WpfApplicationEntity.API.Employee objectEmployee = new WpfApplicationEntity.API.Employee();
                        objectEmployee.fName = "User";
                        objectEmployee.name = "Admin";
                        objectEmployee.lName = "Userski";
                        objectEmployee.position = "Администратор";
                        objectEmployee.login = "Admin";
                        objectEmployee.password = "admin";
                        objectEmployee.birth_date = "02.06.2021";
                        objectEmployee.address = "home";
                        objectEmployee.phone = "543543";
                        objectEmployee.position_set_date = "03.06.2021";
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
