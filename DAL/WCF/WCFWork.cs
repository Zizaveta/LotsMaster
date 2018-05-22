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
		string AddPerson(string FirstName, string SecondName,  string Email, string Password, bool Gender = false, byte[] Img = null);
		[OperationContract]
		string Authorization(string email, string password);
        [OperationContract]
        string AddLot(string Name, string About, int StartPrice, DateTime Start, DateTime Finish,byte[] Img = null);
        [OperationContract]
		string Bet(int lotId, int money);
		[OperationContract]
		List<Lot> OldLots();
        [OperationContract]
        List<Lot> AllLots();
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
        [OperationContract]
        void SingOut();

    }



    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class AuctionClient : IAuctionClient
	{
		public Person person;

        public void SingOut()
        {
            person = null;
        }

        public Lot AboutLot(int LotId)
        {
            return ClassWork.AboutLot(LotId);
        }


        public string AddLot(string Name, string About, int StartPrice, DateTime Start, DateTime Finish, byte[] Img = null)
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

        public string AddPerson(string FirstName, string SecondName, string Email, string Password, bool Gender = false, byte[] Img =  null)
		{
			if (Email.Length < 8 || Email.Contains("@") == false || Email.Contains(".") == false)
				return "Wrong email";
			if (Password.Length < 8)
				return "Password must have minimum 8 sumvols";
			if (FirstName.Length == 0 || SecondName.Length == 0)
				return "You need input FirstName and SecondName";
            return ClassWork.AddPerson(new Person() { Email = Email, Password = Password, FirstName = FirstName, SecondName = SecondName, Gender = Gender, Image = Img });
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
            return ClassWork.Bet(person, lotId, money);
				
		}

		public void ForgetPassword(string email, string Name)
		{
			ClassWork.ForgetPassword(email, Name);
		}

		public List<Lot> FutureLots()
		{
            try
            {
                //List<Lot> Lots = ClassWork.FutureLots();
                //foreach (Lot lot in Lots)
                //{
                //    lot.WhoSale.Password = null;
                //    lot.History = null;
                //    lot.Tells = null;
                //    Log.Logger(lot.LotName);
                //}
                //return Lots;
                return ClassWork.FutureLots();
            }
            catch(Exception ex)
            {
                Log.Logger(ex.Message);
                return null;
            }
		}

		public List<Lot> NowLots()
		{
            try
            {
                //List<Lot> Lots = ClassWork.NowLots();

                //foreach (Lot lot in Lots)
                //{
                //    lot.WhoSale.Password = null;
                //    lot.History = null;
                //    lot.Tells = null;
                //    Log.Logger(lot.LotName);
                //}
                //return Lots;
                return ClassWork.NowLots();
            }
            catch(Exception ex)
            {
                Log.Logger(ex.Message);
                return null;
            }
		}

		public List<Lot> OldLots()
		{
            try
            {
                //List<Lot> Lots = ClassWork.OldLots();
                //foreach (Lot lot in Lots)
                //{
                //    lot.WhoSale.Password = null;
                //    lot.History = null;
                //    lot.Tells = null;
                //    Log.Logger(lot.LotName);
                //}
                //return Lots;

                return ClassWork.OldLots();
            }
            catch (Exception ex)
            {
                Log.Logger(ex.Message);
                return null;
            }
        }

        public List<Lot> AllLots()
        {
            return ClassWork.AllLots();
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
			return ClassWork.TellMeAboutStartLot(person, LotId);
		}

		public int LastBet(int LotId)
		{
			return ClassWork.LastBet(LotId);
		}
	}
}
