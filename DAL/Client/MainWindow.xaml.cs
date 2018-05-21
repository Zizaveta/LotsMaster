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
using System.Windows.Threading;

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
      
            try
            {
                DateTime dt1 = new DateTime(2018, 5, 18, 17, 56, 43);
                DateTime dt2 = new DateTime(2018, 5, 18, 17, 56, 47);
                TimeSpan ts = dt2 - dt1;
                LotsTimer.Tick += Timer_Tick;
                LotsTimer.Interval = ts;
                LotsTimer.Start();
            }
            catch (Exception ex)
            {

                Clipboard.SetText(ex.Message);
                MessageBox.Show(ex.Message);
            }
            

            //GridInfoTextBlock.Width = GridInfo.ActualWidth;
            Client = new AuctionClientClient();
            //Client.NowLots().Last().History.Last().Money;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("Yeeeee");
            ((DispatcherTimer)sender).Stop();
        }
        private void MainTimer(object sender, EventArgs e)
        {
            
            //TimeToEndElement.Text = LotsTimer.
        }

        

        private void LoadInfoAboutAllLots()
        {
            //Client.
            //Get Name
            //Get Image 
            //Get Current Price

            //tag = id
            int i = 1;
            while(i!=5)
            {
            BitmapImage bi1 = new BitmapImage();
            bi1.BeginInit();
            bi1.UriSource = new Uri("TempPhotoForTest/Lots"+i+ ".jpg", UriKind.Relative);
            bi1.EndInit();
            switch (i)
            {
                case 1: Ph1.Source = bi1;break;
                case 2: Ph2.Source = bi1; break;
                case 3: Ph3.Source = bi1; break;
                case 4: Ph4.Source = bi1; break;
                default:Console.WriteLine("Default case");break;
            }
                i++;
            }
         

            //ph1-8 Photos
            //SettingsImg2 - Name
        }

        private void OpenLot()
        {
            //Client.
            //Get Name
            //Get Image and Info
            //Get Current Price
        }

        //Image-Info click
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

        //RedLotPrice
        private void timerTick(object sender, EventArgs e)
        {
            MyCurrentPrice.Background =  new SolidColorBrush(Colors.Red);
            System.Windows.Threading.DispatcherTimer DeleteRed = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timerEnd);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Start();
        }
        private void timerEnd(object sender, EventArgs e)
        {
            MyCurrentPrice.Background = new SolidColorBrush(Colors.White);
        }

        //TextBox ENTER
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

        //WindowsMove
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                SingInWindow.Visibility = Visibility.Visible;
                SingUpWindow.Visibility = Visibility.Hidden;
                AutorizedWindow.Visibility = Visibility.Hidden;
                ForgotPasswordWindow.Visibility = Visibility.Hidden;
                SettingWindows.Visibility = Visibility.Hidden;
            }
            if (e.Key == Key.F2)
            {
                SingInWindow.Visibility = Visibility.Hidden;
                SingUpWindow.Visibility = Visibility.Visible;
                AutorizedWindow.Visibility = Visibility.Hidden;
                ForgotPasswordWindow.Visibility = Visibility.Hidden;
                SettingWindows.Visibility = Visibility.Hidden;
            }
            if (e.Key == Key.F3)
            {
                SingInWindow.Visibility = Visibility.Hidden;
                SingUpWindow.Visibility = Visibility.Hidden;
                AutorizedWindow.Visibility = Visibility.Hidden;
                ForgotPasswordWindow.Visibility = Visibility.Visible;
                SettingWindows.Visibility = Visibility.Hidden;
            }
            if (e.Key == Key.F4)
            {
                SingInWindow.Visibility = Visibility.Hidden;
                SingUpWindow.Visibility = Visibility.Hidden;
                AutorizedWindow.Visibility = Visibility.Hidden;
                ForgotPasswordWindow.Visibility = Visibility.Hidden;
                SettingWindows.Visibility = Visibility.Visible;
                LoadInfoAboutAllLots();
            }
            if (e.Key == Key.F5)
            {
                SingInWindow.Visibility = Visibility.Hidden;
                SingUpWindow.Visibility = Visibility.Hidden;
                AutorizedWindow.Visibility = Visibility.Visible;
                ForgotPasswordWindow.Visibility = Visibility.Hidden;
                SettingWindows.Visibility = Visibility.Hidden;
            }
            if (e.Key == Key.Escape)
            {
                if (ForgotPasswordWindow.Visibility == Visibility.Visible || SingUpWindow.Visibility == Visibility.Visible)
                {

                    SingInWindow.Visibility = Visibility.Visible;
                    SingUpWindow.Visibility = Visibility.Hidden;
                    AutorizedWindow.Visibility = Visibility.Hidden;
                    ForgotPasswordWindow.Visibility = Visibility.Hidden;
                }

                //if (ForgotPasswordWindow.Visibility == Visibility.Visible || SingUpWindow.Visibility == Visibility.Visible)
                //{
                //}
            }
        }
        private void SingIn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SingInWindow.Visibility = Visibility.Visible;
            SingUpWindow.Visibility = Visibility.Hidden;
            AutorizedWindow.Visibility = Visibility.Hidden;
            ForgotPasswordWindow.Visibility = Visibility.Hidden;
        }
        private void SingUp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SingInWindow.Visibility = Visibility.Hidden;
            SingUpWindow.Visibility = Visibility.Visible;
            AutorizedWindow.Visibility = Visibility.Hidden;
            ForgotPasswordWindow.Visibility = Visibility.Hidden;
        }
        private void Forgot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SingInWindow.Visibility = Visibility.Hidden;
            SingUpWindow.Visibility = Visibility.Hidden;
            AutorizedWindow.Visibility = Visibility.Hidden;
            ForgotPasswordWindow.Visibility = Visibility.Visible;
        }

        private void SendPass_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void AddLotButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void SettingButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void SingOutButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ThemesButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void SettingWindows_Loaded(object sender, RoutedEventArgs e)
        {
            LoadInfoAboutAllLots();
        }

        private void OpenLotWindowHandler(object sender, MouseButtonEventArgs e)
        {

            int temp = Convert.ToInt32((sender as Image).Tag);
            switch (temp)
            {
                // юзати autorizedwindow
                case 1:
                    {
                        SingInWindow.Visibility = Visibility.Hidden;
                        SingUpWindow.Visibility = Visibility.Hidden;
                        ForgotPasswordWindow.Visibility = Visibility.Hidden;
                        SettingWindows.Visibility = Visibility.Hidden;
                        AutorizedWindow.Visibility = Visibility.Visible;
                        //Name NameElement
                        //Timer  TimeToEndElement
                        //Photo PhotoInfo
                        //TextInfo GridInfoTextBlock
                        //Price CurrentPrice, який спочатку є стартовим
                        //Log LogWindow




                    }
                    break;
                case 2:
                    {
                   
                    } break;
                case 3:
                    {
                   
                    } break;
                case 4:
                    {

                    }
                    break;
                default: break;
            }
        }
    }
}
