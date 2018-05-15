using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
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
					return true;
				}
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
				return false;
			}
		}
		public static bool Bet(LotHistory history)
		{
			try
			{
				using (AuctionContent db = new AuctionContent())
				{
					db.History.Add(history);
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
    }
}
