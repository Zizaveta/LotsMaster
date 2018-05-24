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
    /// Логика взаимодействия для ForgetPassw.xaml
    /// </summary>
    public partial class ForgetPassw : Window
    {
        public ForgetPassw()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          MessageBox.Show(  MainWindow.client.ForgetPassword(mail.Text, name.Text));
            this.Close();
        }
    }
}
