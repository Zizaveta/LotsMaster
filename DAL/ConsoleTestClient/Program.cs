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

            try
            {
                AuctionClientClient client = new AuctionClientClient();
                //Console.WriteLine(	 client.AddPerson("Liza", "Rengan", "miss.elizaveta.rengan@gmail.com", "12345678", false, null));

                Console.WriteLine(client.Authorization("miss.elizaveta.rengan@gmail.com", "12345678"));
                // Console.WriteLine(client.AddLot("Автомобіль ВАЗ413 ", "Стан хороший. 23л", 2200, new DateTime(2018, 06,18,21,52,0), new DateTime(2018, 06,21, 20,0,0), null ));

                foreach (Lot elem in client.FutureLots())
                {
                    Console.WriteLine(elem.LotName);
                }
                //foreach (Lot elem in client.NowLots())
                //{
                //    Console.WriteLine(elem);
                //}
                //foreach (Lot elem in client.OldLots())
                //{
                //    Console.WriteLine(elem);
                //}

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }

    }
}
