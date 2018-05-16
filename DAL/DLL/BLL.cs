using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Logger;
namespace BLL
{
    public class ClassWork // пропоную тут зробити найпростіші ф-ції, а всю перевірку накатати в WCF
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
		public static bool AddLot(Lot lot)
		{
			try
			{
				using (AuctionContent db = new AuctionContent())
				{
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
		public static bool Bet(Person p, string lotName, int money)
		{
			try
			{
				using (AuctionContent db = new AuctionContent())
				{
					if (db.Lots.FirstOrDefault(elem => elem.LotName == lotName) == null)
						return false;
					if (db.Lots.FirstOrDefault(elem => elem.LotName == lotName).History.Last().Money > money)
						return false;
					if (db.Lots.FirstOrDefault(elem => elem.LotName == lotName).TimeFinish < DateTime.Now)
						return false;
					if (db.Lots.FirstOrDefault(elem => elem.LotName == lotName).TimeStart > DateTime.Now)
						return false;
						db.History.Add(new LotHistory() { Persson = p, Money = money, Lot = db.Lots.FirstOrDefault(elem => elem.LotName == lotName) });
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
					return db.Lots.Where(elem => elem.TimeStart > DateTime.Now).ToList();
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
					return db.Lots.Where(elem => elem.TimeStart > DateTime.Now && elem.TimeFinish < DateTime.Now).ToList();
				}
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
				return null;
			}
		}
		public static bool TellMeAboutStartLot(Person person, string lot)
		{
			try
			{
				using (AuctionContent db = new AuctionContent())
				{
					if (db.Lots.First(elem => elem.LotName == lot).TimeStart <= DateTime.Now)
						return false;
					db.Lots.First(elem => elem.LotName == lot).TellPersonsAboutStart.Add(person);
					db.SaveChanges();
					return true;
				}
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
				return false;
			}
		}
    }

	class ServiceWork
	{
		public void TellForPersonAboutStartLot()
		{
			using (AuctionContent db = new AuctionContent())
			{
				//foreach(Lot elem in db.Lots.Where())
			}
		}
	}
}
