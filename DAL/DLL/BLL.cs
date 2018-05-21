using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Logger;
using System.Net.Mail;
using System.Windows.Threading;

namespace BLL
{
    public class ClassWork 
    {
		public static bool AddPerson(Person person)
		{
			try
			{
					using (AuctionContent db = new AuctionContent())
					{
					
						if (db.Persons.FirstOrDefault(elem => elem.Email == person.Email) == null)
						{
							db.Persons.Add(person);
							db.SaveChanges();
                        Log.Logger("Person add");
							return true;
						}
						return false;
					
				}
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
				return false;
			}
		}
		public static Person Authorization(string email, string password)
		{
			try
			{
				using (AuctionContent db = new AuctionContent())
				{
					return db.Persons.FirstOrDefault(elem => elem.Email == email && elem.Password == password);
				}
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
				return null;
			}
		}
		public static bool AddLot(Lot lot,Person p)
		{
			try
			{
				using (AuctionContent db = new AuctionContent())
				{
                    db.Persons.Attach(p);
                    lot.WhoSale = p;
                    //message
					db.Lots.Add(lot);
					db.SaveChanges();
					return true;
				}
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
				return false;
			}
		}
		public static bool Bet(Person p, int lotId, int money)
		{
			try
			{
				using (AuctionContent db = new AuctionContent())
				{
					if (db.Lots.FirstOrDefault(elem => elem.Id == lotId) == null)
						return false;
					if (db.Lots.FirstOrDefault(elem => elem.Id == lotId).History.Last().Money > money)
						return false;
					if (db.Lots.FirstOrDefault(elem => elem.Id == lotId).TimeFinish < DateTime.Now)
						return false;
					if (db.Lots.FirstOrDefault(elem => elem.Id == lotId).TimeStart > DateTime.Now)
						return false;
						db.History.Add(new LotHistory() { Persson = p, Money = money, Lot = db.Lots.FirstOrDefault(elem => elem.Id == lotId) });
					db.SaveChanges();
					return true;
				}
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
				return false;
			}
		}
		public static List<Lot> OldLots()
		{
			try
			{
				using (AuctionContent db = new AuctionContent())
				{
					return db.Lots.Where(elem => elem.TimeFinish < DateTime.Now).ToList();
				}
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
				return null;
			}
		}
		public static List<Lot> FutureLots()
		{
			try
			{
				using (AuctionContent db = new AuctionContent())
				{
					return db.Lots.Where(elem => elem.TimeStart > DateTime.Now).OrderBy(elem => elem.TimeStart).ToList();
                }
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
				return null;
			}
		}
		public static List<Lot> NowLots()
		{
			try
			{
				using (AuctionContent db = new AuctionContent())
				{
					return db.Lots.Where(elem => elem.TimeStart > DateTime.Now && elem.TimeFinish < DateTime.Now).OrderBy(elem => elem.TimeStart).ToList();
				}
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
				return null;
			}
		}
        //public static bool TellMeAboutStartLot(Person person, int lotId)
        //{
        //	try
        //	{
        //		using (AuctionContent db = new AuctionContent())
        //		{
        //			if (db.Lots.First(elem => elem.Id == lotId).TimeStart <= DateTime.Now)
        //				return false;
        //			if (db.Lots.First(elem => elem.Id == lotId).TellPersonsAboutStart.Contains(person) == false)
        //			{
        //				db.Lots.First(elem => elem.Id == lotId).TellPersonsAboutStart.Add(person);
        //				db.SaveChanges();
        //			}
        //			return true;
        //		}
        //	}
        //	catch(Exception ex)
        //	{
        //		Log.Logger(ex.Message);
        //		return false;
        //	}
        //}
        
        public static void TellMeAboutStartLot(Lot temp)
        {
            try
            {
             DispatcherTimer timer = new DispatcherTimer();
                EventArgs ea = new EventArgs();
                timer.Tick += Timer_Tick;
                DateTime dt1 = temp.TimeStart;
                DateTime dt2 = temp.TimeFinish;
                TimeSpan ts = dt2 - dt1;
                timer.Interval = ts;
                timer.Start();

            }
            catch (Exception ex)
            {
                Log.Logger(ex.Message);
            }

        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            //пройтися циклом і повідомити людей
            //for
            //db.Lots.First(elem => elem.Id == lotId).TellPersonsAboutStart.Add(person);
            ((DispatcherTimer)sender).Stop();
        }

        //Треба ліст людей

        public static void SendMessage(Person from, string Thema, string Message, Person to)
		{
			Task.Run(() =>
			{
				try
				{
					MailMessage m = new MailMessage(new MailAddress(from.Email, from.FirstName + " " + from.SecondName), new MailAddress(to.Email));
					m.Subject = Thema;
					m.Body = Message;


					SmtpClient smtp = new SmtpClient("aspmx.l.google.com", 25);
					smtp.EnableSsl = true;
					smtp.Send(m);
				}
				catch (Exception ex)
				{
					Log.Logger(ex.Message);
				}
			});
		}
		public static bool ForgetPassword(string email, string FN)
		{
			try
			{
				using (AuctionContent db = new AuctionContent())
				{
					ClassWork.SendMessage(new Person() { Email = "miss.elizaveta@gmail.com", FirstName = "Not", SecondName = "Name" }, "ForgetPassword", "Your password: " + db.Persons.First(elem => elem.Email == email && elem.FirstName == FN).Password, db.Persons.First(elem => elem.Email == email));
					return true;
				}
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
				return false;
			}
		}
		public static string LotHistory(int lotId)
		{
			try
			{
				using (AuctionContent db = new AuctionContent())
				{
					string str = "";
					Lot h = db.Lots.First(elem => elem.Id == lotId);
					foreach(LotHistory elem in h.History)
					{
						str += elem.Persson.FirstName + "\t" + elem.Money + "\n";
					}
					return str;
				}
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
				return "";
			}
		}
		public static int LastBet(int LotId)
		{
			try
			{
				using (AuctionContent db = new AuctionContent())
				{
					return db.Lots.First(elem => elem.Id == LotId).History.Last().Money;
				}
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
				return 0;
			}
		}
        public static Lot AboutLot(int LotId)
        {
            try
            {
                using (AuctionContent db = new AuctionContent())
                {
                    Lot l =  db.Lots.FirstOrDefault(elem => elem.Id == LotId);
                    l.WhoSale.Password = null;
                    l.History = null;
                    foreach (Person elem in l.TellPersonsAboutStart)
                    {
                        elem.Lots = null;
                        elem.Password = null;
                        elem.Histories = null;
                    }
                    return l;
                }
            }
            catch(Exception ex)
            {
                Log.Logger(ex.Message);
                return null;
            }
        }
	}




	public class ServiceWork
	{
        public void TellForPersonAboutStartLot()
        {
            Task.Run(() =>
            {
                using (AuctionContent db = new AuctionContent())
                {
                    foreach (Lot elem in db.Lots.Where(elem => elem.TellPersonsAboutStart != null && elem.TimeStart >= DateTime.Now))
                    {
                        foreach (Person p in elem.TellPersonsAboutStart)
                        {
                            ClassWork.SendMessage(new Person() { Email = "miss.elizaveta@gmail.com", FirstName = "Not ", SecondName = "Empty" }, "Lot is start", "We want to tell you about start lot  " + elem.LotName + "Now", p);
                        }
                        elem.TellPersonsAboutStart = null;
                    }
                    db.SaveChanges();
                }
            });
        }
    }
}
