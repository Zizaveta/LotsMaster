using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTestClient.ServiceReference1;
namespace ConsoleTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            AuctionClientClient client = null;
            try
            {
                client = new AuctionClientClient();
                //Console.WriteLine(	 client.AddPerson("Liza", "Rengan", "miss.elizaveta.rengan@gmail.com", "12345678", false, null));
                //client.ForgetPassword("miss.elizaveta.rengan@gmail.com", "Liza");

                Console.WriteLine(client.Authorization("miss.elizaveta.rengan@gmail.com", "12345678"));
                //Console.WriteLine(client.AddLot("Автомобіль ВАЗ413 ", "Стан хороший. 23л", 2200, new DateTime(2018, 06,18,21,52,0), new DateTime(2018, 06,21, 20,0,0), null ));
                //Console.WriteLine(client.ChangePassword("12345678"));
                //Console.WriteLine(client.AddLot("New lot#1","test lot", 100, new DateTime(2018,05,24,17,12,0), new DateTime(2018, 05, 24, 17, 12, 0), null));
                //Console.WriteLine(client.ChangeFirstName("Elizaveta"));
                //foreach (Lot elem in client.FutureLots())
                //{
                //    Console.WriteLine(elem.LotName);
                //}

                Console.WriteLine(client.Bet(1, 500));
                //Console.WriteLine(client.TellMeAboutStartLot(1));
                Console.WriteLine(client.LotHistory(1));
                //Console.WriteLine(client.LastBet(1));
                //Console.WriteLine(client.AboutLot(1).LotName);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
