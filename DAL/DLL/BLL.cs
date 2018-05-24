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
    public class LotsReturn
    {
        public static List<Lot> OldLots()
        {
            try
            {
                using (AuctionContent db = new AuctionContent())
                {
                    return db.Lots.Where(elem => elem.TimeFinish < DateTime.Now).OrderBy(elem => elem.TimeStart).ToList();
                }
            }
            catch (Exception ex)
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
                    return db.Lots.Where(elem => elem.TimeStart > DateTime.Now).OrderBy(elem => elem.TimeStart).OrderBy(elem => elem.TimeStart).ToList();
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
                    return db.Lots.Where(elem => elem.TimeStart > DateTime.Now && elem.TimeFinish < DateTime.Now).OrderBy(elem => elem.TimeStart).OrderBy(elem => elem.TimeStart).ToList();
                }
            }
            catch (Exception ex)
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
                    return db.Lots.OrderBy(elem => elem.TimeStart).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Logger(ex.Message);
                return null;
            }
        }

    }
    public class PersonWork
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
            catch (Exception ex)
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
                    db.Persons.Attach(person);
                    if (db.Lots.First(elem => elem.Id == lotId).TimeStart <= DateTime.Now)
                        return "Lot was started";
                    if (db.Tells.FirstOrDefault(elem => elem.Lot.Id == lotId && elem.Person.Id == person.Id) == null)
                    {
                        db.Tells.Add(new Tell() { Person = person, Lot = db.Lots.First(elem => elem.Id == lotId) });
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
            catch (Exception ex)
            {
                Log.Logger(ex.Message);
                return false;
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
        public static string ChangePassword(int PersonId, string NewPassword)
        {
            try
            {
                using (AuctionContent db = new AuctionContent())
                {
                    db.Persons.First(elem => elem.Id == PersonId).Password = NewPassword;
                    db.SaveChanges();
                    return "Password is change";
                }
            }
            catch(Exception ex)
            {
                Log.Logger(ex.Message);
                return "Wrong in db";
            }
        }
        public static string ChangeFirstName(int PersonId, string Name)
        {
            try
            {
                using (AuctionContent db = new AuctionContent())
                {
                    db.Persons.First(elem => elem.Id == PersonId).FirstName = Name;
                    db.SaveChanges();
                    return "Name is change";
                }
            }
            catch (Exception ex)
            {
                Log.Logger(ex.Message);
                return "Wrong in db";
            }
        }
        public static string ChangeSecondName(int PersonId, string Name)
        {
            try
            {
                using (AuctionContent db = new AuctionContent())
                {
                    db.Persons.First(elem => elem.Id == PersonId).SecondName = Name;
                    db.SaveChanges();
                    return "Name is change";
                }
            }
            catch (Exception ex)
            {
                Log.Logger(ex.Message);
                return "Wrong in db";
            }
        }
    }
    public class LotsWork 
    {
		
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
                    ServiceWork.TellAboutStartLot(lot);
                    PersonWork.SendMessage(db.Persons.First(), "Auction", "Your lot " + lot.LotName + " is add. Wait for start)", p);
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
                    if (db.Lots.FirstOrDefault(elem => elem.Id == lotId).TimeFinish < DateTime.Now)
                        return "Lot is finished";
					if (db.Lots.FirstOrDefault(elem => elem.Id == lotId).TimeStart > DateTime.Now)
						return "Lot is not start";
                    if (LastBet(lotId) != 0 && LastBet(lotId)>=money)
                        return "Small bet";
                    LotHistory l = new LotHistory() { Persson = p, Money = money, Lot = db.Lots.FirstOrDefault(elem => elem.Id == lotId) };
                    db.History.Add(l);
                    db.Lots.First(elem => elem.Id == lotId).History.Add(l);
					db.SaveChanges();
                    return "You make Bet!)";
				}
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
				return "Something wrong with db";
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
						str += elem.Persson.FirstName + "\t - \t" + elem.Money + "\n";
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
                    return l;
                }
            }
            catch(Exception ex)
            {
                Log.Logger(ex.Message);
                return null;
            }
        }
        public static List<LotHistory> GetLotHistory(int lot)
        {
            try
            {
                using (AuctionContent db = new AuctionContent())
                {
                    return db.Lots.FirstOrDefault(elem => elem.Id == lot).History.ToList();
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
        public static List<DispatcherTimer> timers = new List<DispatcherTimer>();
        //треба ліст піпл
        public static void TellAboutStartLot(Lot temp)
        {
            try
            {
                Log.Logger("Work ServiceWork.TellMeAboutStartLot");

                timers.Add(new DispatcherTimer());

                timers[timers.Count - 1].Tick += Timer_Tick;
                timers[timers.Count - 1].Tag = temp.Id;
                DateTime dt1 = DateTime.Now;
                DateTime dt2 = temp.TimeStart;
                TimeSpan ts = dt2 - dt1;

                Log.Logger(ts.ToString());

                //DateTime dt1 = temp.TimeStart;
                //DateTime dt2 = temp.TimeFinish;
                //TimeSpan ts = dt2 - dt1;
                timers[timers.Count - 1].Interval = ts;
                timers[timers.Count - 1].Start();
                Log.Logger("Finish Work ServiceWork.TellMeAboutStartLot");

            }
            catch (Exception ex)
            {
                Log.Logger(ex.Message);
                Log.Logger("Exep ServiceWork.TellMeAboutStartLot");
            }

        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Log.Logger("Work ServiceWork.Timer_Tick");
            ServiceWork.TellForPersonAboutStartLot((int)((DispatcherTimer)sender).Tag);
            ((DispatcherTimer)sender).Stop();
            timers.Remove(((DispatcherTimer)sender));
            Log.Logger("Finish Work ServiceWork.Timer_Tick");

        }
        public static void TellForPersonAboutStartLot(int lotId)
        {
            Log.Logger("Work ServiceWork.TellForPersonAboutStartLot");
            using (AuctionContent db = new AuctionContent())
            {
                foreach (Tell elem in db.Tells.Where(elem => elem.Lot.Id == lotId))
                {
                    PersonWork.SendMessage(db.Persons.First(), "Lot is start", "We want to tell you about start lot  " + elem.Lot.LotName + " NOW!!! Hurry up make your bet)", elem.Person);
                }
            }
            Log.Logger("Finish work ServiceWork.TellForPersonAboutStartLot");
        }

        //public static void TellAll()
        //{
        //    try
        //    {
        //        using (AuctionContent db = new AuctionContent())
        //        {
        //            for(;;)
        //            foreach (Lot elem in db.Lots.ToList())
        //            {
        //                if(elem.TimeStart.Year == DateTime.Now.Year && elem.TimeStart.Month == DateTime.Now.Month && elem.TimeStart.Day == DateTime.Now.Day && elem.TimeStart.Hour == DateTime.Now.Hour && elem.TimeStart.Minute == DateTime.Now.Minute)
        //                {
        //                    foreach (Tell elemTell in db.Tells.ToList())
        //                    {
        //                        PersonWork.SendMessage(db.Persons.First(), "Start lot", "Lot '" + elem.LotName + " is start NOW!!!", elemTell.Person);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        Log.Logger("Service exception: " + ex.Message);
        //    }
        //}
    }
}
