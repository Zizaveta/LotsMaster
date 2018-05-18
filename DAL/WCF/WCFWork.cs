using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using DAL;
using BLL;
using System.Net.Mail;
using Logger;
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
		string AddLot(string Name, string About, int StartPrice, DateTime Start, DateTime Finish, string Img = null);
		[OperationContract]
		string Bet(int lotId, int money);
		[OperationContract]
		List<Lot> OldLots();
		[OperationContract]
		List<Lot> FutureLots();
		[OperationContract]
		List<Lot> NowLots();
		[OperationContract]
		string TellMeAboutStartLot(int LotId);
		[OperationContract]
		void SendMessage(string Thema, string Message, Person to);
		[OperationContract]
		void ForgetPassword(string email, string FirstName);
		[OperationContract]
		string LotHistory(int LotId);
		[OperationContract]
		int LastBet(int LotId);
        [OperationContract]
        Lot AboutLot(int LotId);

    }



    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class AuctionClient : IAuctionClient
	{
		public Person person;

        public Lot AboutLot(int LotId)
        {
            return ClassWork.AboutLot(LotId);
        }


        public string AddLot(string Name, string About, int StartPrice, DateTime Start, DateTime Finish, string Img = null)
		{
			if (person == null)
				return "Authorization!!!";
			if (StartPrice <= 0)
				return "Start price must be up 0";
			if (Finish < Start || Start < DateTime.Now)
				return "Input true date and time";
			if (Name.Length == 0 || About.Length == 0)
				return "Input name and about";
			if (ClassWork.AddLot(new Lot() { About = About, LotName = Name, StartPrice = StartPrice, TimeStart = Start, TimeFinish = Finish, Photo = Img }, person) == true)
				return "Lot is add";
			else return "Something wrong";
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

		public string Bet(int lotId, int money)
		{
			if (person == null)
				return "Authorization!!!";
			if (ClassWork.Bet(person, lotId, money) == true)
				return "All ok";
			return "Something wrong";
				
		}

		public void ForgetPassword(string email, string Name)
		{
			ClassWork.ForgetPassword(email, Name);
		}

		public List<Lot> FutureLots()
		{
			List<Lot> Lots = ClassWork.FutureLots();
			foreach(Lot lot in Lots)
			{
                lot.WhoSale.Password = null;
                lot.History = null;
				lot.TellPersonsAboutStart = null;
			}
			return Lots;
		}

		public List<Lot> NowLots()
		{
            List<Lot> Lots = ClassWork.NowLots();
            if (Lots.Count < 5)
                Lots.AddRange(ClassWork.FutureLots());
            foreach (Lot lot in Lots)
			{
                lot.WhoSale.Password = null;
				lot.History = null;
				lot.TellPersonsAboutStart = null;
			}
			return Lots;
		}

		public List<Lot> OldLots()
		{
			List<Lot> Lots = ClassWork.OldLots();
			foreach (Lot lot in Lots)
			{
                lot.WhoSale.Password = null;
                lot.History = null;
				lot.TellPersonsAboutStart = null;
			}
			return Lots;
		}

		public string LotHistory(int LotId)
		{
			return ClassWork.LotHistory(LotId);
		}

		public void SendMessage(string Thema, string Message, Person to)
		{
			ClassWork.SendMessage(person, Thema, Message, to);
		}

		public string TellMeAboutStartLot(int LotId)
		{
			if (ClassWork.TellMeAboutStartLot(person, LotId) == true)
				return "All ok";
			return "Somethimg wrong";
		}

		public int LastBet(int LotId)
		{
			return ClassWork.LastBet(LotId);
		}
	}
}
