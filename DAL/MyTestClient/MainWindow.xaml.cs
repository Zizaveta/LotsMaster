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
using MyTestClient.ServiceReference1;
namespace MyTestClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static AuctionClientClient client;
        public MainWindow()
        {
            InitializeComponent();
            client = new AuctionClientClient();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           string str =  client.Authorization(mail.Text, pass.Password);
            if (str == "Authorization")
            {
                MenWindow w = new MenWindow();
                w.ShowDialog();
            }
            else MessageBox.Show(str);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Regist w = new Regist();
            w.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ForgetPassw w = new ForgetPassw();
            w.ShowDialog();
        }
    }
}
