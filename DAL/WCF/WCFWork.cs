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
		string ForgetPassword(string email, string FirstName);
		[OperationContract]
		string LotHistory(int LotId);
		[OperationContract]
		int LastBet(int LotId);
        [OperationContract]
        Lot AboutLot(int LotId);
        [OperationContract]
        void SingOut();
        [OperationContract]
        string ChangePassword(string NewPassword);
        [OperationContract]
        string ChangeFirstName(string Name);
        [OperationContract]
        string ChangeSecondName(string Name);
        [OperationContract]
        List<LotHistory> GetLotHistory(int lot);


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
            Lot l = LotsWork.AboutLot(LotId);
            return new Lot() { LotName = l.LotName, StartPrice = l.StartPrice, About = l.About, Id = l.Id, Photo = l.Photo, TimeFinish = l.TimeFinish, TimeStart = l.TimeStart};
        }

        public List<LotHistory> GetLotHistory(int lot)
        {
            try
            {
                List<LotHistory> l = new List<LotHistory>();
                foreach(LotHistory elem in LotsWork.GetLotHistory(lot))
                {
                    l.Add(new LotHistory() { Id = elem.Id, Persson = new Person() { FirstName = elem.Persson.FirstName }, Money = elem.Money });
                }
                return l;
            }
            catch(Exception ex)
            {
                Log.Logger(ex.Message);
                return null;
            }
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
            if (LotsWork.AddLot(new Lot() { About = About, LotName = Name, StartPrice = StartPrice, TimeStart = Start, TimeFinish = Finish, Photo = Img }, person) == true)
                return "Lot is add";
            else return "Something wrong";
        }

        public string AddPerson(string FirstName, string SecondName, string Email, string Password, bool Gender = false, byte[] Img = null)
        {
            if (Email.Length < 8 || Email.Contains("@") == false || Email.Contains(".") == false)
                return "Wrong email";
            if (Password.Length < 8)
                return "Password must have minimum 8 sumvols";
            if (FirstName.Length == 0 || SecondName.Length == 0)
                return "You need input FirstName and SecondName";
            return PersonWork.AddPerson(new Person() { Email = Email, Password = Password, FirstName = FirstName, SecondName = SecondName, Gender = Gender, Image = Img });
        }

        public string Authorization(string email, string password)
        {
            person = PersonWork.Authorization(email, password);
            if (person == null)
                return "Wrong email or password";
            return "Authorization";
        }

        public string Bet(int lotId, int money)
        {
            if (person == null)
                return "Authorization!!!";
            return LotsWork.Bet(person, lotId, money);
        }

        public string ForgetPassword(string email, string Name)
        {
            if (PersonWork.ForgetPassword(email, Name) == true)
                return "Your password send on email";
            return "Wrong Name or Email";
        }

        public List<Lot> FutureLots()
        {
            try
            {
                List<Lot> Lots = new List<Lot>();
                foreach (Lot lot in LotsReturn.FutureLots())
                {
                    Lots.Add(new Lot() { Id = lot.Id, LotName = lot.LotName, About = lot.About, Photo = lot.Photo, StartPrice = lot.StartPrice, TimeStart = lot.TimeStart, TimeFinish = lot.TimeFinish});
                }
                return Lots;
            }
            catch (Exception ex)
            {
                Log.Logger(ex.Message);
                return null;
            }
        }

        public List<Lot> NowLots()
        {
            try
            {
                List<Lot> Lots = new List<Lot>();
                foreach (Lot lot in LotsReturn.NowLots())
                {
                    Lots.Add(new Lot() { Id = lot.Id, LotName = lot.LotName, About = lot.About, Photo = lot.Photo, StartPrice = lot.StartPrice, TimeStart = lot.TimeStart, TimeFinish = lot.TimeFinish });
                }
                return Lots;
            }
            catch (Exception ex)
            {
                Log.Logger(ex.Message);
                return null;
            }
        }

        public List<Lot> OldLots()
        {
            try
            {
                List<Lot> Lots = new List<Lot>();
                foreach (Lot lot in LotsReturn.OldLots())
                {
                    Lots.Add(new Lot() { Id = lot.Id, LotName = lot.LotName, About = lot.About, Photo = lot.Photo, StartPrice = lot.StartPrice, TimeStart = lot.TimeStart, TimeFinish = lot.TimeFinish });
                }
                return Lots;
            }
            catch (Exception ex)
            {
                Log.Logger(ex.Message);
                return null;
            }
        }

        public List<Lot> AllLots()
        {
            try
            {
                List<Lot> Lots = new List<Lot>();
                foreach (Lot lot in LotsReturn.AllLots())
                {
                    Lots.Add(new Lot() { Id = lot.Id, LotName = lot.LotName, About = lot.About, Photo = lot.Photo, StartPrice = lot.StartPrice, TimeStart = lot.TimeStart, TimeFinish = lot.TimeFinish });
                }
                return Lots;
            }
            catch (Exception ex)
            {
                Log.Logger(ex.Message);
                return null;
            }
        }

        public string LotHistory(int LotId)
        {
            return LotsWork.LotHistory(LotId);
        }

        public void SendMessage(string Thema, string Message, Person to)
        {
            PersonWork.SendMessage(person, Thema, Message, to);
        }

        public string TellMeAboutStartLot(int LotId)
        {
            return PersonWork.TellMeAboutStartLot(person, LotId);
        }

        public int LastBet(int LotId)
        {
            return LotsWork.LastBet(LotId);
        }

        public string ChangePassword(string NewPassword)
        {
            if (person == null)
                return "Autorizat!!!";
            if (NewPassword.Length < 8)
                return "Small password";
            if (PersonWork.ChangePassword(person.Id, NewPassword) == "Password is change")
            {
                person.Password = NewPassword;
                return "Password is change";
            }
            return "Password is change";
        }

        public string ChangeFirstName(string Name)
        {
            if (person == null)
                return "Autorizat!!!";
            if (Name.Length < 8)
                return "Small name";
            if (PersonWork.ChangeFirstName(person.Id, Name) == "Name is change")
            {
                person.FirstName = Name;
                return "Name is change";
            }
            return "Name is change";
        }

        public string ChangeSecondName(string Name)
        {
            if (person == null)
                return "Autorizat!!!";
            if (Name.Length < 8)
                return "Small name";
            if (PersonWork.ChangeSecondName(person.Id, Name) == "Name is change")
            {
                person.SecondName = Name;
                return "Name is change";
            }
            return "Name is change";
        }
    }
}
