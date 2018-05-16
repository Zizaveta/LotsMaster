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
using Client.ServiceReference1;
namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuctionClientClient Client;
        public MainWindow()
        {
            InitializeComponent();
            //GridInfoTextBlock.Width = GridInfo.ActualWidth;
            Client = new AuctionClientClient();
            //Client.NowLots().Last().History.Last().Money;
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridImage.Visibility = Visibility.Visible;
            GridInfo.Visibility = Visibility.Hidden;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridImage.Visibility = Visibility.Hidden;
            GridInfo.Visibility = Visibility.Visible;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int tempCountMoney = Convert.ToInt32(MyCurrentPrice.Text);
                if (Convert.ToInt32(CurrentPrice.Text) < tempCountMoney)
                //if (Client.NowLots().Last().History.Last().Money < tempCountMoney)
                {
                    //Треба переаисати на Ід
                    //Client.Bet(Client.NowLots().Last().History.Last().Lot.Id, Convert.ToInt32(MyCurrentPrice.Text));
                    {
                        CurrentPrice.Text = tempCountMoney.ToString();
                    }
                }
                else
                MessageBox.Show("<");
            }
        }
    }
}
