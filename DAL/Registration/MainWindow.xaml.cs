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
using Registration.ServiceReference1;

namespace Registration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuctionClientClient a = new AuctionClientClient();
        public void Reg()
        {
            string name,surname, email, password;
            name = Name.Text;
            surname = Surname.Text;
            email = Email.Text;
            password = Password.Password;
            MessageBox.Show(a.AddPerson(name, surname, email, password));
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Sign in
        }
    }
}
