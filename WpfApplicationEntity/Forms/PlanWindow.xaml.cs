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
    /// Логика взаимодействия для PlanWindow.xaml
    /// </summary>
    public partial class PlanWindow : Window
    {
        private bool add_edit;
        public PlanWindow()
        {
            InitializeComponent();
        }
        public PlanWindow(bool add_edit)
        {
            InitializeComponent();
            this.add_edit = add_edit;
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (this.add_edit == true)
                if (date.Text != string.Empty
                    && shop.Text != string.Empty)
                {
                    WpfApplicationEntity.API.Production_plan objectPlan = new WpfApplicationEntity.API.Production_plan();
                    objectPlan.date = date.Text;
                    //objectPlan.shop = shop.Text;                    
                    try
                    {
                        using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                            new WpfApplicationEntity.API.MyDBContext())
                        {
                            objectMyDBContext.Production_plans.Add(objectPlan);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("План добавлен");
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
