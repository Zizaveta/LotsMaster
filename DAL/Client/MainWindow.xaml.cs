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
using System.Globalization;
namespace Client
{
    public partial class MainWindow : Window
    {
        AuctionClientClient Client;
        public System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public System.Windows.Threading.DispatcherTimer LotsTimer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            //string date1 = "6:50:14";
            //date1, CultureInfo.CreateSpecificCulture("en-US")
            //, CultureInfo.CreateSpecificCulture("en-US").Calendar, DateTimeKind.Utc
            try
            {
                     //time = new DateTime(1,1,1,6,50,20,DateTimeKind.Local); //Work with 1,1,1 only...
                     //TimeToEndElement.Text = time.ToShortTimeString();
            }
            catch (Exception ex)
            {

                Clipboard.SetText(ex.Message);
                MessageBox.Show(ex.Message);
            }
            LotsTimer.Tick += new EventHandler(MainTimer);
            LotsTimer.Interval = new TimeSpan(0, 0, 0, 1);
            LotsTimer.Start();

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
        private void timerTick(object sender, EventArgs e)
        {
            MyCurrentPrice.Background =  new SolidColorBrush(Colors.Red);
            System.Windows.Threading.DispatcherTimer DeleteRed = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timerEnd);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Start();
        }
        private void MainTimer(object sender, EventArgs e)
        {
            
            //TimeToEndElement.Text = LotsTimer.
        }

        private void timerEnd(object sender, EventArgs e)
        {
            MyCurrentPrice.Background = new SolidColorBrush(Colors.White);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int tempCountMoney = Convert.ToInt32(MyCurrentPrice.Text);
                if (Convert.ToInt32(CurrentPrice.Text) < tempCountMoney)
                //if (Client.NowLots().Last().History.Last().Money < tempCountMoney)
                {
                  //Client.Bet(Client.NowLots().Last().History.Last().Lot.Id, Convert.ToInt32(MyCurrentPrice.Text));
                    {
                        CurrentPrice.Text = tempCountMoney.ToString();
                    }
                }
                else
                {
                    timer.Tick += new EventHandler(timerTick);
                    timer.Interval = new TimeSpan(0, 0, 0,0,70);
                    timer.Start();
                }
            }
        }

    }
}
