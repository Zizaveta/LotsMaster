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
			AuctionClientClient client = new AuctionClientClient();
				Console.WriteLine(	 client.AddPerson("Liza", "Rengan", "miss.elizaveta.rengan@gmail.com", "12345678"));
			//Console.WriteLine(client.Authorization("miss.elizaveta.rengan@gmail.com", "12345678"));
		}
	}
}
