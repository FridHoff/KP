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
    /// Логика взаимодействия для ShipmentWindow.xaml
    /// </summary>
    public partial class ShipmentWindow : Window
    {
        private bool add_edit;
        public ShipmentWindow()
        {
            InitializeComponent();
        }
        public ShipmentWindow(bool add_edit)
        {
            InitializeComponent();
            this.add_edit = add_edit;
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (this.add_edit == true)
                if (receiving_date.Text != string.Empty
                    && departuring_date.Text != string.Empty
                    && count.Text != string.Empty
                    && order.Text != string.Empty)
                {
                    WpfApplicationEntity.API.Shipment objectShipment = new WpfApplicationEntity.API.Shipment();
                    objectShipment.departure_date = departuring_date.Text;
                    objectShipment.receiving_date = receiving_date.Text;
                    objectShipment.count = Convert.ToInt32(count.Text);
                    try
                    {
                        using (WpfApplicationEntity.API.MyDBContext objectMyDBContext =
                            new WpfApplicationEntity.API.MyDBContext())
                        {
                            objectMyDBContext.Shipments.Add(objectShipment);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Запись отгрузки добавлена");
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
