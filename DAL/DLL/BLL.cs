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
		public static string AddPerson(Person person)
		{
			try
			{
					using (AuctionContent db = new AuctionContent())
					{

                    if (db.Persons.FirstOrDefault(elem => elem.Email == person.Email) != null)
                    {
                        return "This email is in db";
                    }
                    else
                    {
                        db.Persons.Add(person);
                        db.SaveChanges();
                        return "Peron add";
                    }
					
				}
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
				return "Wrong in db";
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
					db.Lots.Add(lot);
					db.SaveChanges();
                    ServiceWork.TellMeAboutStartLot(lot);
                    SendMessage(db.Persons.First(), "Auction", "Your lot is add in db", p);
                    return true;
				}
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
				return false;
			}
		}
		public static string Bet(Person p, int lotId, int money)
		{
			try
			{
				using (AuctionContent db = new AuctionContent())
				{
                    db.Persons.Attach(p);
                    if (db.Lots.FirstOrDefault(elem => elem.Id == lotId) == null)
						return "Wrong id of lot";
                    if (db.Lots.FirstOrDefault(elem => elem.Id == lotId).History.Last().Money > money)
                        return "Small bet";
                    if (db.Lots.FirstOrDefault(elem => elem.Id == lotId).TimeFinish < DateTime.Now)
                        return "Lot is finished";
					if (db.Lots.FirstOrDefault(elem => elem.Id == lotId).TimeStart > DateTime.Now)
						return "Lot is not start";
                    LotHistory l = new LotHistory() { Persson = p, Money = money, Lot = db.Lots.FirstOrDefault(elem => elem.Id == lotId) };
                    db.History.Add(l);
                    db.Lots.First(elem => elem.Id == lotId).History.Add(l);
					db.SaveChanges();
                    return "Lot is create";
				}
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
				return "Something wrong with db";
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
                    //return db.Lots.Where(elem => elem.TimeStart > DateTime.Now).OrderBy(elem => elem.TimeStart).ToList();
                    return db.Lots.ToList();
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
        public static string TellMeAboutStartLot(Person person, int lotId)
        {
            try
            {
                using (AuctionContent db = new AuctionContent())
                {
                    if (db.Lots.First(elem => elem.Id == lotId).TimeStart <= DateTime.Now)
                        return "Lot was started";
                    if (db.Tells.FirstOrDefault(elem => elem.Lot.Id == lotId && elem.Person.Id == person.Id)==null)
                    {
                        db.Tells.Add(new Tell() { Person = person, Lot = db.Lots.First(elem => elem.Id==lotId) });
                        db.SaveChanges();
                    }
                    return "You will know about start event";
                }
            }
            catch (Exception ex)
            {
                Log.Logger(ex.Message);
                return "Something wrong in db";
            }
        }
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
					SendMessage(db.Persons.First(), "ForgetPassword", "Your password: " + db.Persons.First(elem => elem.Email == email && elem.FirstName == FN).Password, db.Persons.First(elem => elem.Email == email));
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
                    return l;
                }
            }
            catch(Exception ex)
            {
                Log.Logger(ex.Message);
                return null;
            }
        }
        public static List<Lot> AllLots()
        {
            try
            {
                using (AuctionContent db = new AuctionContent())
                {
                    return db.Lots.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Logger(ex.Message);
                return null;
            }
        }
	}




	public class ServiceWork
	{
        //треба ліст піпл
        public static void TellMeAboutStartLot(Lot temp)
        {
            try
            {
                DispatcherTimer timer = new DispatcherTimer();
                EventArgs ea = new EventArgs();
                timer.Tick += Timer_Tick;
                DateTime dt1 = DateTime.Now;
                DateTime dt2 = temp.TimeStart;
                TimeSpan ts = dt2 - dt1;
                //DateTime dt1 = temp.TimeStart;
                //DateTime dt2 = temp.TimeFinish;
                //TimeSpan ts = dt2 - dt1;
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
            ServiceWork.TellForPersonAboutStartLot();
            ((DispatcherTimer)sender).Stop();
        }
        public static void TellForPersonAboutStartLot()
        {
            Task.Run(() =>
            {
                using (AuctionContent db = new AuctionContent())
                {
                    foreach (Tell elem in db.Tells.Where(elem => elem.Lot.TimeStart >= DateTime.Now))
                    {
                            ClassWork.SendMessage(db.Persons.First(), "Lot is start", "We want to tell you about start lot  " + elem.Lot.LotName + " NOW!!!", elem.Person);
                    }
                }
            });
        }
    }
}
