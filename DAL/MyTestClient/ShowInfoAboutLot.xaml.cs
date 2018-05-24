using MyTestClient.ServiceReference1;
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
    /// Логика взаимодействия для ShowInfoAboutLot.xaml
    /// </summary>
    public partial class ShowInfoAboutLot : Window
    {
        int lotId;
        public ShowInfoAboutLot(int LotId)
        {
            InitializeComponent();
            lotId = LotId;
            Lot l = MainWindow.client.AboutLot(LotId);
            LName.Text = l.LotName;
            LAbout.Text = l.About;
            LStart.Text = l.TimeStart.ToString();
            LFinish.Text = l.TimeStart.ToString();
            LStartPrice.Text = l.StartPrice.ToString();
            LHistory.Text = MainWindow.client.LotHistory(lotId);
            LLastBet.Text = MainWindow.client.LastBet(LotId).ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show( MainWindow.client.Bet(lotId, Int32.Parse(LBet.Text)));
        }
    }
}
