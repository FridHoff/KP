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
    /// Логика взаимодействия для GroupWindow.xaml
    /// </summary>
    public partial class GroupWindow : Window
    {
        private bool add_edit;
        public GroupWindow()
        {
            InitializeComponent();
        }
        public GroupWindow(bool add_edit)
        {
            InitializeComponent();
            this.add_edit = add_edit;
        }

        private void ButtonAddEditGroup_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
