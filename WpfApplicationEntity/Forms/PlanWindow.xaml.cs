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
    /// Логика взаимодействия для PlanWindow.xaml
    /// </summary>
    public partial class PlanWindow : Window
    {
        private bool add_edit=false;
        int index;
        public PlanWindow()
        {
            InitializeComponent();
        }
        public PlanWindow(int id)
        {
            InitializeComponent();
            this.add_edit = true;
            index = id;
            using (WpfApplicationEntity.API.MyDBContext objectMyDBContext = new WpfApplicationEntity.API.MyDBContext())
            {
                WpfApplicationEntity.API.Production_plan plan = WpfApplicationEntity.API.DatabaseRequest.GetPlanById(objectMyDBContext, index);
                date.Text = plan.date;
            }
            ButtonAddEdit.Content = "Изменить";        
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (this.add_edit == true)
                if (date.Text != string.Empty
                    && shop.Text != string.Empty)
                {
                    WpfApplicationEntity.API.Production_plan objectPlan = new WpfApplicationEntity.API.Production_plan();
                    objectPlan.date = date.Text;
                    objectPlan.shop = findShop(shop.SelectedItem.ToString());                    
                    try
                    {
                        using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                         new WpfApplicationEntity.API.MyDBContext())
                        {
                            if (add_edit == false)
                                objectMyDBContext.Production_plans.Add(objectPlan);
                            else
                            {
                                objectPlan.ID = index;
                                WpfApplicationEntity.API.Production_plan objectFromDataBase = new WpfApplicationEntity.API.Production_plan();
                                objectFromDataBase = WpfApplicationEntity.API.DatabaseRequest.GetPlanById(objectMyDBContext, index);
                                objectMyDBContext.Entry(objectFromDataBase).CurrentValues.SetValues(objectPlan);
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
                shop.ItemsSource = numbers;
            }
        }
        private Shop findShop(string ProdName)
        {
            Shop prod = new Shop();
            using (MyDBContext DB = new MyDBContext())
            {
                var customers = DatabaseRequest.GetShops(DB);
                foreach (var item in customers)
                {
                    if (ProdName == item.ID.ToString())
                        prod = item;
                }
            }
            return prod;
        }
    }
}
