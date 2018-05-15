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
	public interface IAuctionClient
	{
		[OperationContract]
		string AddPerson(string FirstName, string SecondName, string Email, string Password);
		[OperationContract]
		string Authorization(string email, string password);
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

	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
	public class AuctionClient : IAuctionClient
	{
		public Person person;
		public string AddLot(Lot lot)
		{
			throw new NotImplementedException();
		}

		public string AddPerson(string FirstName, string SecondName, string Email, string Password)
		{
			if (Email.Length < 8 || Email.Contains("@") == false || Email.Contains(".") == false)
				return "Wrong email";
			if (Password.Length < 8)
				return "Password must have minimum 8 sumvols";
			if (FirstName.Length == 0 || SecondName.Length == 0)
				return "You need input FirstName and SecondName";
			if (ClassWork.AddPerson(new Person() { Email = Email, Password = Password, FirstName = FirstName, SecondName = SecondName }) == true)
				return "Person is create";
			else return "Email  was in db or something wrong";

		}

		public string Authorization(string email, string password)
		{
			person = ClassWork.Authorization(email, password);
			if (person == null)
				return "Wrong email or password";
			return "Authorization";
		}

		public string Bet(LotHistory history)
		{
			throw new NotImplementedException();
		}

		public List<Lot> FutureLots()
		{
			return ClassWork.FutureLots();
		}

		public List<Lot> NowLots()
		{
			return ClassWork.NowLots();
		}

		public List<Lot> OldLots()
		{
			return ClassWork.OldLots();
		}
	}
}
