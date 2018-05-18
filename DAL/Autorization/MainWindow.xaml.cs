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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autorization.ServiceReference1;

namespace Autorization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuctionClientClient a = new AuctionClientClient();
        public void Aut()
        {
            MessageBox.Show(a.Authorization(Email.Text, Password.Password));
            MessageBox.Show("Complete");
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
