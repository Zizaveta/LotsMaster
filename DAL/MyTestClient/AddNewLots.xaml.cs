using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyTestClient
{
    /// <summary>
    /// Логика взаимодействия для AddNewLots.xaml
    /// </summary>
    public partial class AddNewLots : Window
    {
        public AddNewLots()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           MessageBox.Show( MainWindow.client.AddLot(Lname.Text, Labout.Text, Int32.Parse(LstartP.Text), Lstart.SelectedDate.Value, Lfinish.SelectedDate.Value, null));
        }
    }
}
