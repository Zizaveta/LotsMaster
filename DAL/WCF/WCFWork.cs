using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using DAL;
using BLL;
namespace WCF
{
	[ServiceContract]
	public interface AuctionClient
	{
		[OperationContract]
		string AddPerson(Person person);
		[OperationContract]
		Person Authorization(string email, string password);
		[OperationContract]
		string AddLot(Lot lot);
		[OperationContract]
		string Bet(LotHistory history);
		[OperationContract]
		List<Lot> OldLots();
		[OperationContract]
		List<Lot> FutureLots();
		[OperationContract]
		List<Lot> NowLots();
	}

    public class WCFWork
    {

    }
}
